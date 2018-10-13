using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth_Console
{
    interface ICreature
    {
        //property signatures
        #region property signatures
        //hp, hpmax, mp, mpmax, atk, def, speed
        string name
        {
            get;
            set;
        }

        int id
        {
            get;
            set;
        }

        int level
        {
            get;
            set;
        }
        
        int hpmax
        {
            get;
            set;
        }

        int hp
        {
            get;
            set;
        }

        int mpmax
        {
            get;
            set;
        }

        int mp
        {
            get;
            set;
        }

        int atk
        {
            get;
            set;
        }

        int def
        {
            get;
            set;
        }

        int speed
        {
            get;
            set;
        }
        #endregion
    }
    
    public class Creature: ICreature
    {
        //Fields
        private string _name;
        private int _id;
        private int _level;
        private int _hp;
        private int _hpmax;
        private int _mp;
        private int _mpmax;
        private int _atk;
        private int _def;
        private int _speed;

        //constructor
        public Creature(string name, int id, int level, int hpmax, int mpmax, int atk, int def, int speed)
        {
            _name = name;
            _id = id;
            _level = level;
            _hpmax = hpmax;
            _hp = hpmax;
            _mpmax = mpmax;
            _mp = mpmax;
            _atk = atk;
            _def = def;
            _speed = speed;
        }

        //Property implimentation
        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public int level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
            }
        }

        public int hpmax
        {
            get
            {
                return _hpmax;
            }
            set
            {
                _hpmax = value;
            }
        }

        public int hp
        {
            get
            {
                return _hp;
            }
            set
            {
                _hp = value;
            }
        }

        public int mpmax
        {
            get
            {
                return _mpmax;
            }
            set
            {
                _mpmax = value;
            }
        }

        public int mp
        {
            get
            {
                return _mp;
            }
            set
            {
                _mp = value;
            }
        }

        public int atk
        {
            get
            {
                return _atk;
            }
            set
            {
                _atk = value;
            }
        }

        public int def
        {
            get
            {
                return _def;
            }
            set
            {
                _def = value;
            }
        }

        public int speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
            }
        }
    }
}

