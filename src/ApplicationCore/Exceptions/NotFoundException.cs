using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Exceptions
{
    public class NotFoundException:Exception
    {
        public NotFoundException(int id)
            :base($"No such record with id {id}")
        {
        }
    }
}
