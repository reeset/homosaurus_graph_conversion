# homosaurus_graph_conversion

Sample process for converting homosaurus json-ld to marcxml (full vocabulary). 

# Steps

1. Download the json ld file (http://homosaurus.org/v2.jsonld)
2. In MarcEdit.  Open MARC Tools and select the JSON=>XML conversion.  Convert json-ld file to xml.
3. Using the console app (this project) -- convert the data to marcxml
4. If you need MARC data -- convert the MARCXML data to MARC in MarcEdit (or your favorite MARC processor)

# Notes

This project uses the 4.8 .NET framework.  The reason, .NETCore has removed the ability to utilize embedded scripts in XSLT due to security concerns.  In .NET core, the script elements would need to be removed, and modified to use XSLT Extension Objects (https://docs.microsoft.com/en-us/dotnet/standard/data/xml/xslt-extension-objects).  I may provide an updated project designed to work with .NET core later.
