﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace PharmApp.DAL.Entities
{
    public partial class Cart
    {
        public Cart()
        {
            CartItems = new HashSet<CartItem>();
        }

        public Guid Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}