﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagnarockApp.UserNicolai.Model
{
    public class User
    {
        //Instance field
        private string _name;
        private int _id;
        private bool _administrator;
        private string _userName;
        private string _code;

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
            set
            {
                _administrator = value;
            }
        }

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        public string Code
        {
            get { return _code;}
            set { _code = value; }
        }


        //Constructor
        public User()
        {

        }

        public User(string name, int id, bool administrator, string userName, string code)
        {
            _name = name;
            _id = id;
            _administrator = administrator;
            _userName = userName;
            _code = code;
        }
    }
}

