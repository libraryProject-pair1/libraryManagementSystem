﻿using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Publisher : Entity<Guid>
{
    public string PublisherName { get; set; }
    public string Adress {  get; set; }
    public string Phone { get; set; }
}
