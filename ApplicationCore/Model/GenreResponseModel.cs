﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Model
{
    public class GenreResponseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<MovieCardResponseModel> Movies { get; set; }
}
}
