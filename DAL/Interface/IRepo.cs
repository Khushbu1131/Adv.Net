﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    
        public interface IRepo<CLASS, ID, RET>
        {
            RET Create(CLASS obj);
            RET Update(CLASS obj);
            CLASS Get(ID id);
            List<CLASS> Get();
            bool Delete(ID id);


        }
    }

