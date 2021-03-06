﻿using Newtonsoft.Json;
using System;

namespace 个人信息数据库.model
{
    [Serializable]
    public class caddressBook: notify_property, ICloneable
    {
        public caddressBook()
        {

        }
        /// <summary>
        /// 标识符
        /// </summary>
        public string id
        {
            set
            {
                _id = value;
                OnPropertyChanged();
            }
            get
            {
                return _id;
            }
        }
        /// <summary>
        /// 通讯人姓名
        /// </summary>
        public string name
        {
            set
            {
                _name = value;
                OnPropertyChanged();
            }
            get
            {
                return _name;
            }
        }
        /// <summary>
        /// 联系方式
        /// </summary>
        public string contact
        {
            set
            {
                _contact = value;
                OnPropertyChanged();
            }
            get
            {
                return _contact;
            }
        }
        /// <summary>
        /// 工作地点
        /// </summary>
        public string address
        {
            set
            {
                _address = value;
                OnPropertyChanged();
            }
            get
            {
                return _address;
            }
        }
        /// <summary>
        /// 城市
        /// </summary>
        public string city
        {
            set
            {
                _city = value;
                OnPropertyChanged();
            }
            get
            {
                return _city;
            }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string comment
        {
            set
            {
                _comment = value;
                OnPropertyChanged();
            }
            get
            {
                return _comment;
            }
        }
        /// <summary>
        /// 输入正确
        /// </summary>
        public bool accord
        {
            set
            {
                value = false;
            }
            get
            {
                if (string.IsNullOrEmpty(name))
                {
                    return false;
                }                
                return true;
            }
        }
        public object Clone()
        {
            caddressBook addressbook = new caddressBook();
            addressbook.id = id;
            addressbook.name = name;
            addressbook.contact = contact;
            addressbook.address = address;
            addressbook.city = city;
            addressbook.comment = comment;
            return addressbook;
        }

        public object Clone(caddressBook addressbook)
        {
            if (addressbook == null)
            {
                addressbook = new caddressBook();
            }
            addressbook.id = id;
            addressbook.name = name;
            addressbook.contact = contact;
            addressbook.address = address;
            addressbook.city = city;
            addressbook.comment = comment;
            return addressbook;
        }

         // override object.Equals
        public override bool Equals(object obj)
        {
            //       
            // See the full list of guidelines at
            //   http://go.microsoft.com/fwlink/?LinkID=85237  
            // and also the guidance for operator== at
            //   http://go.microsoft.com/fwlink/?LinkId=85238
            //

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            caddressBook addressbook = obj as caddressBook;
            if (addressbook.id != id)
            {
                return false;
            }
            if (addressbook.name != name)
            {
                return false;
            }
            if (addressbook.contact != contact)
            {
                return false;
            }
            if (addressbook.address != address)
            {
                return false;
            }
            if (addressbook.city != city)
            {
                return false;
            }
            if (addressbook.comment != comment)
            {
                return false;
            }
            // TODO: write your implementation of Equals() here
            return true;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
        private string _id;
        private string _name;
        private string _contact;
        private string _address;
        private string _city;
        private string _comment;
    }
}
