﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GargabeCollectTest
{
    public interface UserClassInterface
    {
        void Run();
    }

    public class UserClassA : UserClassInterface
    {
        int _parent = -1;
        int _reference = -1;
        static int _referenceCount = 0;

        public void Run()
        {
            Console.WriteLine("Call (" + _parent + ")/A.Run(" + (_reference) + ")");
        }

        public UserClassA(int parent)
        {
            _parent = parent;
            _reference = _referenceCount++;
            Console.WriteLine("Call (" + _parent + ")/A.Construction (" + (_reference) + ")");
        }

        ~UserClassA() 
        {
            Console.WriteLine("Call (" + _parent + ")/A.Destruction (" + (_reference) + ")");
        }
    }

    public class UserClassB : UserClassInterface
    {
        int _parent = -1;
        int _reference = -1;
        static int _referenceCount = 0;

        public void Run()
        {
            Console.WriteLine("Call (" + _parent + ")/B.Run (" + (_reference) + ")");
        }

        public UserClassB(int parent)
        {
            _reference = _referenceCount++;
            _parent = parent;
            Console.WriteLine("Call ("+_parent+")/B.Construction (" + (_reference) + ")");
        }

        ~UserClassB()
        {
            Console.WriteLine("Call (" + _parent + ")/B.Destruction (" + (_reference) + ")");
        }
    }

    public class RutineClass
    {
        int _reference = 0;
        const int _dataCount = 5;

        public void Run()
        {
            Console.WriteLine("1.RutineClass GC.TotalMemory({0}kb)", GC.GetTotalMemory(false) * 0.001f);
            Console.WriteLine("1.RutineClass GC.TotalMemory({0}kb)", GC.GetTotalMemory(true) * 0.001f);
            Console.WriteLine("1.RutineClass GC.TotalMemory({0}kb)", GC.GetTotalMemory(false) * 0.001f);

            List<UserClassA> listUserGroup = new List<UserClassA>();

            for (int i = 0; i < _dataCount; i++)
            {
                UserClassA userclass = new UserClassA(_reference);
                listUserGroup.Add(userclass);
            }

            listUserGroup.ForEach(u => u.Run());
            listUserGroup.Clear();
            //GC.Collect();

            List<UserClassB> listUserGroupB = new List<UserClassB>();
            Random random = new Random();
            for (int i = 0; i < _dataCount; ++i)
            {
                UserClassB userclass = new UserClassB(_reference);
                listUserGroupB.Add(userclass);
            }

            listUserGroupB.ForEach(u => u.Run());
            listUserGroupB.Clear();
            //GC.Collect();

            Console.WriteLine("2.RutineClass GC.TotalMemory({0}kb)", GC.GetTotalMemory(false) * 0.001f);
            Console.WriteLine("2.RutineClass GC.TotalMemory({0}kb)", GC.GetTotalMemory(true) * 0.001f);
            Console.WriteLine("2.RutineClass GC.TotalMemory({0}kb)", GC.GetTotalMemory(false) * 0.001f);
        }

        public RutineClass( int reference )
        {
            _reference = reference;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 1; i++)
            {
                RutineClass rutine = new RutineClass(i);
                rutine.Run();
                rutine = null;
                GC.Collect(0, GCCollectionMode.Forced);
                GC.WaitForFullGCComplete();
                //GC.Collect();
            }

            return;
        }
        
    }
}



