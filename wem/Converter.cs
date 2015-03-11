
namespace wem
{


    public class Converter
    {

        public static string MediaWikiToXHTML(string markup)
        {
            string retVal = null;

            using (XhtmlPrinter printer = new XhtmlPrinter())
            {
                java.io.Reader rdr = new java.io.StringReader(markup);

                // org.wikimodel.wem.WikiPrinter wp = new org.wikimodel.wem.WikiPrinter ();
                // var listener = new org.wikimodel.wem.xwiki.XWikiSerializer(wp);

                // var listener = new org.wikimodel.wem.xwiki.XWikiSerializer(printer);
                org.wikimodel.wem.IWemListener listener = new org.wikimodel.wem.xhtml.PrintListener(printer);

                org.wikimodel.wem.mediawiki.MediaWikiParser mep =
                    new org.wikimodel.wem.mediawiki.MediaWikiParser();
                mep.parse(rdr, listener);
                retVal = printer.Text;

                rdr.close();
                rdr = null;
                listener = null;
                mep = null;
            } // End Using printer

            return retVal;
        }


        public static string MediaWikiToXhtmlPage(string markup, string title)
        {
            title = System.Web.HttpUtility.HtmlEncode(title);

            return string.Format(@"<!DOCTYPE html>
<html>
<head>
	<meta http-equiv=""X-UA-Compatible"" content=""IE=edge,chrome=1""  />
	 <meta http-equiv=""Content-Type"" content=""text/html;charset=utf-8"" />
	 <meta charset=""UTF-8"" /> 
	<title>{0}</title>
</head>
<body>
	{1}
</body>
</html>
", title, MediaWikiToXHTML(markup));
        }


        public static void MediaWiki2XhtmlFile(string filename, string title, string markup)
        {
            System.IO.File.WriteAllText(filename, MediaWikiToXhtmlPage(markup, title), System.Text.Encoding.UTF8);
        }


        public static void MediaWiki2XhtmlFragmentTest()
        {

            

            System.Console.WriteLine(MediaWikiToXHTML("=== bold ==="));

            System.Console.WriteLine(MediaWikiToXHTML("''italic''"));


            string table = @"
{|
|Orange||Apple||more
|-
|Bread||Pie||more
|-
|Butter||Ice<br/>cream||and<br/>more
|}
";

            System.Console.WriteLine(MediaWikiToXHTML(table));


            string enumeration = @"
# Start each line
# with a [[Wikipedia:Number_sign|number sign]] (#).
## More number signs give deeper
### and deeper
### levels.
# Line breaks <br />don't break levels.
### But jumping levels creates empty space.
# Blank lines

# end the list and start another.
Any other start also
ends the list.

<script type=""text/javascript"">
alert('hello');
alert(""helläöüo"");
</script>
";
            System.Console.WriteLine(MediaWikiToXHTML(enumeration));


        }


        public static void MediaWiki2XhtmlFileTest()
        {
            string rd = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            rd = System.IO.Path.Combine(rd, "../..");
            rd = System.IO.Path.Combine(rd, "test.htm");
            rd = System.IO.Path.GetFullPath(rd);

            MediaWiki2XhtmlFile(rd, "MediaWiki Test", TestData.MediaWikiFormatting);
        }


    }


}
