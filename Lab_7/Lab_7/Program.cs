using System;

namespace lab7_ads
{
    class Date
    {
        public Date(int y, string m, int d)
        {
            this.year = y;
            this.month = m;
            this.day = d;
            this.i = 1;
        }
        int year;
        string month;
        int day;
        public int i;
        public int GetHashDate()
        {
            return Convert.ToInt32(year + "" + day);
        }
        public override string ToString()
        {
            return year.ToString() + month + day.ToString();
        }
        public static bool Equals(Date d1, Date d2)
        {
            if (d1.day == d2.day && d1.month == d2.month && d1.year == d2.year)
            {
                return true;
            }
            return false;
        }
        public Date Clone()
        {
            return new Date(year, month, day);
        }
        public void AddDays(int days)
        {
            for (int i = 0; i < days; i++)
            {
                if ((this.month == "January" || this.month == "March"
                    || this.month == "May" || this.month == "July"
                    || this.month == "August" || this.month == "October")
                    && this.day == 31)
                {
                    this.day = 1;
                    this.month = NextMonth(this.month);
                }
                else if ((this.month == "April" || this.month == "June"
                    || this.month == "September" || this.month == "November")
                    && this.day == 30)
                {
                    this.day = 1;
                    this.month = NextMonth(this.month);
                }
                else if (this.month == "December" && this.day == 31)
                {
                    this.day = 1;
                    this.month = NextMonth(this.month);
                    this.year++;
                }
                else if (this.month == "February")
                {
                    if (this.year % 4 == 0 && this.year % 100 != 0)
                    {
                        if (this.day == 29)
                        {
                            this.day = 1;
                            this.month = NextMonth(this.month);
                        }
                        else this.day++;
                    }
                    else
                    {
                        if (this.day == 28)
                        {
                            this.day = 1;
                            this.month = NextMonth(this.month);
                        }
                        else this.day++;
                    }
                }
                else this.day++;
            }
        }
        public string NextMonth(string month)
        {
            string res = "";
            switch (month)
            {
                case "January":
                    res = "February";
                    break;
                case "February":
                    res = "March";
                    break;
                case "March":
                    res = "April";
                    break;
                case "April":
                    res = "May";
                    break;
                case "May":
                    res = "June";
                    break;
                case "June":
                    res = "July";
                    break;
                case "July":
                    res = "August";
                    break;
                case "August":
                    res = "September";
                    break;
                case "September":
                    res = "October";
                    break;
                case "October":
                    res = "November";
                    break;
                case "November":
                    res = "December";
                    break;
                case "December":
                    res = "January";
                    break;
            }
            return res;
        }
    }
    class Guest
    {
        public Guest(string m, int n, string r, Date d, int nn)
        {
            this.mainGuestName = m;
            this.numberOfGuests = n;
            this.roomType = r;
            this.dateOfArrival = d;
            this.numberOfNights = nn;
        }
        public string mainGuestName;
        public int numberOfGuests;
        public string roomType;
        public Date dateOfArrival;
        public int numberOfNights;
    }
    class Key
    {
        public int key;
        public int i;
        public Key(int key)
        {
            this.key = key;
            this.i = 1;
        }
    }
    class Entry
    {
        public Key key;
        public Guest value;
        public Entry(Key k, Guest v)
        {
            this.key = k;
            this.value = v;
        }
        public override string ToString()
        {
            return $"{key.key} - {value.mainGuestName}, number of guests - {value.numberOfGuests}, number of nights - {value.numberOfNights}";
        }
    }
    class HashTable
    {
        public int size;
        public int loadness;
        private Entry[] table;
        public int currentBC;

        public HashTable(int size)
        {
            this.size = size;
            this.loadness = 0;
            this.table = new Entry[size];
            this.currentBC = 001000;
        }
        public AdditionalHashTable AllDeparturesAtThisDate(Date date)
        {
            AdditionalHashTable hs = new AdditionalHashTable(this.size);
            int counter = 1;
            for (int i = 0; i < size; i++)
            {
                if (table[i] != null)
                {
                    Date departDate = table[i].value.dateOfArrival.Clone();
                    departDate.AddDays(table[i].value.numberOfNights);
                    if (Date.Equals(departDate, date))
                    {
                        if (counter == 1) Console.WriteLine($"All departures at {date}: ");
                        AdditionalEntry entry = new AdditionalEntry(departDate, table[i].key.key);
                        hs.InsertEntry(entry);
                        Console.WriteLine($"{counter}) Booking code: {table[i].key.key}" +
                            $"\n\rMain guest name: {table[i].value.mainGuestName}\n\rRoom type: {table[i].value.roomType}.");
                        counter += 1;
                    }
                }
            }
            if (counter == 1) Console.WriteLine($"Departures at {date} were not found.");
            return hs;
        }
        public HashTable Rehash()
        {
            HashTable newhs = new HashTable(this.size * 2);
            for (int i = 0; i < this.size; i++)
            {
                if (table[i] != null)
                {
                    newhs.InsertEntry(table[i]);
                }
            }
            return newhs;
        }
        public void InsertEntry(Entry e)
        {
            int pos = HashCode(e.key);
            while (pos == -1)
            {
                e.key.key += 1;
                pos = HashCode(e.key);
            }
            table[pos] = e;
            this.loadness += 1;
        }
        public bool RemoveEntry(Key k)
        {
            while (true)
            {
                int pos = GetHash(k);
                if (table[pos] == null) return false;
                else
                {
                    if (table[pos].key.key == k.key)
                    {
                        table[pos] = null;
                        this.loadness -= 1;
                        return true;
                    }
                    k.i += 1;
                }
            }
        }
        public Entry FindEntry(Key k)
        {
            while (true)
            {
                int pos = GetHash(k);
                if (table[pos] == null) return null;
                else
                {
                    if (table[pos].key.key == k.key) return table[pos];
                    k.i += 1;
                }
            }
        }
        public int GetHash(Key k)
        {
            int hash1 = k.key % 7;
            int hash2 = k.key % 5;
            int hash = (hash1 + k.i * hash2) % size;
            return hash;
        }
        public int HashCode(Key k)
        {
            int hash1 = k.key % 7;
            int hash2 = k.key % 5;
            int hash = (hash1 + k.i * hash2) % size;
            while (table[hash] != null)
            {
                k.i += 1;
                hash = (hash1 + k.i * hash2) % size;
                if (k.i > size) return -1;
            }
            return hash;
        }
        public void Print()
        {
            foreach (Entry e in table)
            {
                if(e != null)
                {
                    Console.WriteLine(e.ToString() + ";");
                }
            }
        }
        
    }
    class AdditionalEntry
    {
        public Date dateOfDeparture;
        public int bookingCode;
        public AdditionalEntry(Date d, int b)
        {
            this.dateOfDeparture = d;
            this.bookingCode = b;
        }
    }
    class AdditionalHashTable
    {
        public int size;
        public int loadness;
        public AdditionalEntry[] table;
        public AdditionalHashTable(int size)
        {
            this.size = size;
            this.table = new AdditionalEntry[size];
        }
        public AdditionalEntry FindEntry(Date d)
        {
            while (true)
            {
                int pos = GetHash(d);
                if (table[pos] == null) return null;
                else
                {
                    if (table[pos].dateOfDeparture == d) return table[pos];
                    d.i += 1;
                }
            }
        }
        public bool RemoveEntry(Date d)
        {
            while (true)
            {
                int pos = GetHash(d);
                if (table[pos] == null) return false;
                else
                {
                    if (table[pos].dateOfDeparture == d)
                    {
                        table[pos] = null;
                        this.loadness -= 1;
                        return true;
                    }
                    d.i += 1;
                }
            }
        }
        public int GetHash(Date d)
        {
            int key = d.GetHashDate();
            int hash1 = key % 7;
            int hash2 = key % 5;
            int hash = (hash1 + d.i * hash2) % size;
            return hash;
        }
        public void InsertEntry(AdditionalEntry e)
        {
            int pos = HashCode(e.dateOfDeparture);
            while (pos == -1)
            {
                e.dateOfDeparture.i += 1;
                pos = HashCode(e.dateOfDeparture);
            }
            table[pos] = e;
            this.loadness += 1;
        }
        public int HashCode(Date d)
        {
            int key = d.GetHashDate();
            int hash1 = key % 7;
            int hash2 = key % 5;
            int hash = (hash1 + d.i * hash2) % size;
            while (table[hash] != null)
            {
                d.i += 1;
                hash = (hash1 + d.i * hash2) % size;
                if (d.i > size) return -1;
            }
            return hash;
        }
    }
    static class ConsoleApp
    {
        static HashTable hs = new HashTable(1021);
        static AdditionalHashTable addhs;
        public static void StartProgram()
        {
            Console.WriteLine("Type !help to see list of commands.");
            while (true)
            {
                string input = Console.ReadLine();
                string[] inputarr = input.Split(" ");
                switch (inputarr[0])
                {
                    case "!help":
                        Console.WriteLine("List of commands:\n\r!add - add new guest / group of guests." +
                            "Arguments: main guest name, number of guests, room type, date of arrival, number of nights" +
                            "Example: !add Artem Kononenko,3,classic,27.May.2022,5" +
                            "\n\r!remove - remove guest by booking code. Example: !remove 123456" +
                            "\n\r!find - find guest by booking code. Example: !find 123456" +
                            "\n\r!allDeparturesAtThisDate - find all departures at this date. Example: !allDeparturesAtThisDate 28.May.2022" +
                            "\n\r!exit - exit app.");
                        break;
                    case "!add":
                        input = input.Substring(5);
                        string[] args = input.Split(",");
                        Guest g = ParseGuest(args);
                        if (g == null) Console.WriteLine("Incorrect guest info input.");
                        else 
                        { 
                            Insert(g);
                            hs.Print();
                        }
                        break;
                    case "!remove":
                        Key k1 = ParseBookingCode(inputarr);
                        if (k1 == null) Console.WriteLine("Incorrect input.");
                        else
                        {
                            Remove(k1);
                            hs.Print();
                        }
                        break;
                    case "!find":
                        Key k2 = ParseBookingCode(inputarr);
                        if (k2 == null) Console.WriteLine("Incorrect input.");
                        else Find(k2);
                        break;
                    case "!allDeparturesAtThisDate":
                        Date d = ParseDate(inputarr);
                        addhs = hs.AllDeparturesAtThisDate(d);
                        break;
                    case "!exit":
                        break;
                    default:
                        Console.WriteLine("Incorrect input");
                        break;
                }
                if (input == "!exit") break;
            }
            Console.WriteLine("Bye!");
        }
        private static Date ParseDate(string[] input)
        {
            if (input.Length > 2) return null;
            string[] datearr = input[1].Split(".");
            if (datearr.Length > 3) return null;
            int day, year;
            if (!int.TryParse(datearr[0], out day) || !int.TryParse(datearr[2], out year)) return null;
            return new Date(year, datearr[1], day);
        }
        private static Key ParseBookingCode(string[] input)// stop here.
        {
            if (input.Length > 2) return null;
            int key;
            if (!int.TryParse(input[1], out key)) return null;
            if (key < 1000 || key > 999999) return null;
            return new Key(key);
        }
        private static Guest ParseGuest(string[] input)
        {
            if (input.Length != 5) return null;
            int guestNum, nightsNum;
            if (!int.TryParse(input[1], out guestNum) || !int.TryParse(input[4], out nightsNum)) return null;
            string[] datearr = input[3].Split(".");
            if (datearr.Length != 3) return null;
            int day, year;
            if (!int.TryParse(datearr[0], out day) || !int.TryParse(datearr[2], out year)) return null;
            Date d = new Date(year, datearr[1], day);
            Guest g = new Guest(input[0], guestNum, input[2], d, nightsNum);
            return g;
        }
        private static void Insert(Guest g)
        {
            Entry e = new Entry(new Key(hs.currentBC++), g);
            hs.InsertEntry(e);
            if (hs.loadness / hs.size > 0.5) hs = hs.Rehash();
        }
        private static void Remove(Key key)
        {
            bool isRemoved = false;
            if (addhs != null)
            {
                Entry e = hs.FindEntry(key);
                isRemoved = hs.RemoveEntry(key);
                Date d = e.value.dateOfArrival;
                d.AddDays(e.value.numberOfNights);
                addhs.RemoveEntry(d);
            }
            else
            {
                Entry e = hs.FindEntry(key);
                isRemoved = hs.RemoveEntry(key);
            }
            if (isRemoved) Console.WriteLine("Entry was successfully removed.");
            else Console.WriteLine("Entry was not found.");
        }
        private static void Find(Key key)
        {
            Entry entry = hs.FindEntry(key);
            if (entry == null) Console.WriteLine("Entry was not found.");
            else Console.WriteLine($"Found an entry.\n\rBooking code: {entry.key.key}" +
                $"\n\rMain guest name: {entry.value.mainGuestName}\n\rRoom type: {entry.value.roomType}.");

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleApp.StartProgram();
        }
    }
}
