using System;

namespace lab1
{
    class Figure
    {
        private double _area;

        public double Area
        {
            get { return _area; }
            set { _area = value; }
        }

        public Figure()
        {
            Console.WriteLine("Конструктор Figure викликаний.");
            _area = 0.0;
        }

        ~Figure()
        {
            Console.WriteLine("Деструктор Figure викликаний.");
        }

        public string GetFigure()
        {
            return $"Фігура з площею: {Area}";
        }
    }
}