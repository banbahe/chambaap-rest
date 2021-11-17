using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace chambapp.bll.Helpers
{
    public class StringHelper
    {
        // stand by method , no working correct 
        public IEnumerable<string> ReadLines(Func<Stream> streamProvider,
            Encoding encoding)
        {
            using (var stream = streamProvider())
                using (var reader = new StreamReader(stream, encoding))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                        yield return line;
                }
            
        }
    }
}
