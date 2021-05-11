using System;
using System.IO;
using System.Net;

namespace homosaurus_xslt_example
{
    class Program
    {
        static void Main(string[] args)
        {
            AppContext.SetSwitch("Switch.System.Xml.AllowDefaultResolver", true);
            string source_file = "";
            string output_file = "";
            string xslt_file = "";

            Console.WriteLine("Full Path to the initial Homosaurus graph file (xml file converted from json-ld:");
            source_file = Console.ReadLine();

            if (string.IsNullOrEmpty(source_file))
            {
                Console.WriteLine("You cannot leave the source file blank....stopping");
                return;
            }
            else if (!System.IO.File.Exists(source_file))
            {
                Console.WriteLine(source_file + " cannot be located...stopping.");
                return;
            }

            Console.WriteLine("Full Path to the save file (where should the marcxml file be saved):");
            output_file = Console.ReadLine();

            if (string.IsNullOrEmpty(output_file))
            {
                Console.WriteLine("You cannot leave the output file blank....stopping");
                return;
            }

            Console.WriteLine("Full Path to the XSLT file used for conversion:");
            xslt_file = Console.ReadLine();

            if (string.IsNullOrEmpty(xslt_file))
            {
                Console.WriteLine("You cannot leave the xslt file blank....stopping");
                return;
            }
            else if (!System.IO.File.Exists(xslt_file))
            {
                Console.WriteLine(xslt_file + " cannot be located...stopping.");
                return;
            }
            //string source_file = @"C:\Users\reese\Downloads\homosaurus_json\homosaurus_graph.xml";
            //string output_file = @"C:\Users\reese\Downloads\homosaurus_json\homosaurus_graph_marcxml.xml";
            //string xslt_file = @"C:\Users\reese\OneDrive\MarcEdit_7\net_marcedit\C#\MProgram\MarcEdit8-beta\exe\bin\Release\net5.0-windows\xslt\homosaurus_xml_graph.xsl";
            // Load the style sheet.
            System.Xml.Xsl.XslCompiledTransform xslt = new System.Xml.Xsl.XslCompiledTransform();
            System.Xml.Xsl.XsltSettings xsltSettings = new System.Xml.Xsl.XsltSettings();
            xsltSettings.EnableDocumentFunction = true;
            xsltSettings.EnableScript = true;

            xslt.Load(xslt_file, xsltSettings, new System.Xml.XmlUrlResolver());

            // Execute the transform and output the results to a file.
            xslt.Transform(source_file, output_file);
            Console.WriteLine("Process has Completed.");
            Console.ReadLine();

        }

        public string GetURL(string myurl)
        {
            var webRequest = WebRequest.Create(myurl);
            using (var response = webRequest.GetResponse())
            using (var content = response.GetResponseStream())
            using (var reader = new StreamReader(content))
            {
                var strContent = reader.ReadToEnd();
                return ExtractPrefLabel(strContent);
            }
            return "";
        }
        public string ExtractPrefLabel(string my_data)
        {
            my_data = my_data.Replace("\r", "");
            my_data = my_data.Replace("\n", "");

            string tmpdata = "";
            if (my_data.IndexOf("skos:prefLabel") > -1)
            {
                tmpdata = my_data.Substring(my_data.IndexOf("skos:prefLabel") + "skos:prefLabel".Length);
                tmpdata = tmpdata.TrimStart(": \"".ToCharArray());
                tmpdata = tmpdata.Substring(0, tmpdata.IndexOf("\""));
                return tmpdata;
            }
            return "";
        }
    }
}
