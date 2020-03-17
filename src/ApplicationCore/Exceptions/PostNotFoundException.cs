using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Exceptions
{
    public class PostNotFoundException:Exception
    {
        public PostNotFoundException(int id)
            :base($"The post with id {id} not found!")
        {
        }
    }
}
