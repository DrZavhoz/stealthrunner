using System.Collections;
using System;

public class EncryptedVariables {


    // Encrypted Variables
    // Written by mx.mex0r

    public struct SafeFloat
    {
        private float offset;
        private float value;

        public SafeFloat (float value = 0)
        {
            offset = UnityEngine.Random.Range(-1000, +1000);
            this.value = value + offset;
        }

        public float GetValue ()
        {
            return value - offset;
        }

        public void Dispose ()
        {
            offset = 0;
            value = 0;
        }

        public override string ToString()
        {
            return GetValue().ToString();
        }

        public static SafeFloat operator +(SafeFloat f1, SafeFloat f2)
        {
            return new SafeFloat(f1.GetValue() + f2.GetValue());
        }

        // ...похожим образом перегружаем остальные операторы
    }

    public struct SafeInt
    {
        private int offset;
        private int value;

        public SafeInt (int value = 0)
        {
            Random rnd = new Random();     
            offset = rnd.Next(-1000, +1000);
            this.value = value + offset;
        }

        public int GetValue ()
        {
            return value - offset;
        }

        public void Dispose ()
        {
            offset = 0;
            value = 0;
        }

        public override string ToString()
        {
            return GetValue().ToString();
        }

        public static SafeInt operator +(SafeInt i1, SafeInt i2)
        {
            return new SafeInt(i1.GetValue() + i2.GetValue());
        }

        public static SafeInt operator -(SafeInt i1, SafeInt i2)
        {
            return new SafeInt(i1.GetValue() - i2.GetValue());
        }

        public static explicit operator double (SafeInt v)
        {
            throw new NotImplementedException();
        }

        public static explicit operator SafeInt(int v)
        {
            throw new NotImplementedException();
        }

        // ...похожим образом перегружаем остальные операторы
    }
}
