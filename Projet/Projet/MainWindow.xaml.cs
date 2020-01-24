﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json.Linq;

namespace Projet
{
    public partial class MainWindow : Window
    {
        public List<ListBoxItem> ProductsList { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            ProductsList= new List<ListBoxItem>();
            this.ListBox1.DataContext = this;
            LoadProducts();
        }
        
        private JToken getAllProducts()
        {
            const string url = "https://fr.openfoodfacts.org/categorie/pains.json";
            var json = new WebClient().DownloadString(url);

            return JObject.Parse(json);
        }

        private void LoadProducts()
        {

            JToken jtokens = getAllProducts()["products"];

            ListBoxItem product;


            foreach (JToken jtoken in jtokens)
            {
                string name = (string)jtoken["generic_name"];
                string quantity = (string)jtoken["quantity"];

                if (!String.IsNullOrEmpty(name))
                {
                    product = new ListBoxItem();
                    product.Content = name + " : " + quantity;
                    ProductsList.Add(product);
                }
            }
        }
    }
}