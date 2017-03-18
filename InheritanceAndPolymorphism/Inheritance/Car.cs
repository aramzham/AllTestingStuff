namespace Inheritance
{
    class Car
    {
        public Car()
        {
            maxSpeed = 55; //for example
        }
        public Car(int max)
        {
            maxSpeed = max;
        }
        private int currentSpeed;
        private readonly int maxSpeed;
        public int Speed
        {
            get { return currentSpeed; }
            set { currentSpeed = value > maxSpeed ? maxSpeed : value; }
        }
    }
}
