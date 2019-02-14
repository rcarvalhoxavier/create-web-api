using System;
namespace com.createwebapi.model
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class TransactionAttribute : Attribute
    {
    }
}