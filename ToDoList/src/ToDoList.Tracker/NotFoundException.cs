using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.ToDoList.Application
{
    /// <summary>
    /// Exception thrown when an entity is not found in the repository.    
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// ctor
        /// </summary>
        public NotFoundException()
            : base("Entity not found.")
        {

        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="message"></param>
        public NotFoundException(string message)
            : base(message)
        {

        }
    }
}
