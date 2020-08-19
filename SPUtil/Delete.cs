using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPUtil
{
    /*
<?xml version="1.0" encoding="UTF-8"?>
<Batch>
  <Method><SetList>fe8ac4a1-c326-4a89-83ec-0793cdee943e</SetList><SetVar Name="Cmd">Delete</SetVar><SetVar Name="ID">1</SetVar></Method>
  <Method><SetList>fe8ac4a1-c326-4a89-83ec-0793cdee943e</SetList><SetVar Name="Cmd">Delete</SetVar><SetVar Name="ID">3</SetVar></Method>
</Batch>
    */
    public class Delete
    {
        public static string CreateBatch(string listID, IEnumerable<string> itemIDs)
        {
            StringBuilder sb = new StringBuilder(4096);
            sb.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?><ows:Batch OnError=\"Continue\">");
            
            foreach ( string itemID in itemIDs )
            {
                sb.Append(BuildMethod(listID, itemID));
            }

            sb.Append("</ows:Batch>");

            return sb.ToString();
        }
        public static string BuildMethod(string listID, string itemID)
        {
            return
                String.Format($"<Method><SetList>{listID}</SetList><SetVar Name=\"Cmd\">Delete</SetVar><SetVar Name=\"ID\">{itemID}</SetVar></Method>");
        }
    }
}
