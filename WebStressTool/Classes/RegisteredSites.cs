using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStressTool.libs;

namespace WebStressTool.Classes
{
    [Serializable]
    public class RegisteredSites
    {

        public List<Url> URLs = new List<Url>();

        public List<Url> Get()
        {
            return URLs;
        }

        public Url Get(string _unique)
        {
            var id = Int32.Parse(_unique);
            foreach (var item in URLs)
            {
                if (item.unique == id) return item;
            }
            return null;
        }

        public void Add(string href)
        {
            if (string.IsNullOrEmpty(href) || string.IsNullOrWhiteSpace(href)) return;
            URLs.Add(new Url(href));
            functions.GetMainForm().SomethingChanged = true;
        }
        public void Edit(string id, string href)
        {
            var _id = URLs.IndexOf(Get(id));
            if (string.IsNullOrEmpty(href) || string.IsNullOrWhiteSpace(href)) return;
            URLs[_id].href = href;
            functions.GetMainForm().SomethingChanged = true;
        }

        public void Remove(string unique)
        {
            var id = Int32.Parse(unique);
            foreach (var item in URLs)
            {
                if (id == item.unique)
                {
                    URLs.Remove(item); break;
                    functions.GetMainForm().SomethingChanged = true;
                }
            }
        }

        public int MoveElementUp(int index)
        {
            if (index == 0) return -1;
            var up_element = URLs[index - 1];
            URLs[index - 1] = URLs[index];
            URLs[index] = up_element;
            return index - 1;
            functions.GetMainForm(). SomethingChanged = true;
        }
        public int MoveElementDown(int index)
        {
            if (index == URLs.Count) return -1;
            var down_element = URLs[index + 1];
            URLs[index + 1] = URLs[index];
            URLs[index] = down_element;
            return index + 1;
            functions.GetMainForm().SomethingChanged = true;
        }


    }
}
