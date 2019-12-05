using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagnarockApp.UserNicolai.Model
{
    class User
    {
        //Instance field
        private string _name;
        private int _id;
        private bool _administrator;

        //Properties
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }


        public bool Administrator
        {
            get { return _administrator; }
            set { _administrator = value; }
        }


        //Constructor
        public User(string name, int id, bool administrator)
        {
            _name = name;
            _id = id;
            _administrator = administrator;
        }
    }
}

