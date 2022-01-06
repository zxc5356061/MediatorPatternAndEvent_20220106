using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediatorPattern_20220106
{
    internal class Program
    {
        /// <summary>
        /// Member_DataChanged事件有OnMember_DataChanged方法
        /// </summary>
        static void Main(string[] args)
        {
            Member member1 = new Member();
            member1.DataChanged += Member_DataChanged;//訂閱Member_DataChanged事件
            member1.Name = "Tom";

            //Member member2 = new Member();
            //member2.DataChanged += Member_DataChanged;
            //member2.Name = "Amy";

            //VIP vIP = new VIP();
            //vIP.DataChanged += Member_DataChanged;
            //vIP.Name = "我爸連戰";
        }

        private static void Member_DataChanged(Member sender, string message)
        {
            Console.WriteLine($"{sender.Name}, {message}");
        }
    }

    // Two parameters are required:
    // First "sender" represents the trigger objective.
    // Second parameter represents the message that you want to tell.
    delegate void DataChangedHandler(Member sender, string message);

    class Member
    {
        public event DataChangedHandler DataChanged;
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnMember_DataChanged("This event just happened!");
            }
        }

        protected virtual void OnMember_DataChanged(string message)
        {
            if(DataChanged != null)
            {
                //"this" == To send class Member itselt to "delegate void DataChangedHandler();"
                DataChanged(this, message);//transfer ["Member sender", message] to Member_DataChanged()
            }
        }
    }

    //class VIP : Member
    //{
    //    private string _Email;
    //    public string Email
    //    {
    //        get { return _Email; }
    //        set
    //        {
    //            _Email = value;
    //            OnDataChanged("This event happened.");
    //        }
    //    }
    //}
}
