
namespace wem
{


    public class XhtmlPrinter : org.wikimodel.wem.IWikiPrinter, System.IDisposable
    {
        protected bool disposed = false;
        protected System.Text.StringBuilder sb = new System.Text.StringBuilder();


        // Public implementation of Dispose pattern callable by consumers. 
        public void Dispose()
        {
            Dispose(true);
            System.GC.SuppressFinalize(this);
        }


        // Protected implementation of Dispose pattern. 
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here. 
                if (sb != null)
                {
                    sb.Clear();
                    sb = null;
                }
            }

            // Free any unmanaged objects here. 
            disposed = true;
        }


        public void print(string str)
        {
            sb.Append(str);
        }


        public void println(string str)
        {
            sb.Append(str);
            sb.Append("\n");
        }


        public string Text
        {
            get
            {
                return sb.ToString();
            }

        }

    }


}
