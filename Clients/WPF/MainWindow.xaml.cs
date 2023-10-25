using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using VerticalSliceArchitecture.Application.Entities;
using System.IO;
using VerticalSliceArchitecture.Application.Common.Models;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private HttpClient client = new HttpClient();
        public string? ErrorText { get; set; } = string.Empty;
        public string? SuccessText { get; set; } = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            // Update port # in the following line.
            client.BaseAddress = new Uri("https://localhost:7098/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region GetTodoList

        public async Task<TodoList> GetTodoListAsync(string path)
        {
            TodoList todoList = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                todoList = await response.Content.ReadAsAsync<TodoList>();
            }
            else
            {
                ErrorText = response.ReasonPhrase;
            }
            return todoList;
        }
        private async void GetTodoList_OnClick(object sender, RoutedEventArgs e)
        {
            var todoList = await GetTodoListAsync("/api/todo-lists");
            SuccessText = $"'{todoList.Title}' retrieved with {todoList.Items.Count} items";
        }

        #endregion

        #region SameIdInDb

        private async void SameIdInDb_OnClick(object sender, RoutedEventArgs e)
        {
            var result1 = await CreateObjectInErrorAsync(new ObjectInError { Created = DateTime.Now, CreatedBy = "pds", Id = 1, SomeValue = "something" });
            var successText = $"id 1 created successfully: {result1?.PathAndQuery}";
            var result2 = await CreateObjectInErrorAsync(new ObjectInError { Created = DateTime.Now, CreatedBy = "pds", Id = 1, SomeValue = "something else with same id" });
            successText += Environment.NewLine + $"id 1 created successfully: {result1?.PathAndQuery}";
        }

        private async Task<Uri> CreateObjectInErrorAsync(ObjectInError objectInError)
        {
            HttpResponseMessage? response = null;
            try
            {
                response = await client.PostAsJsonAsync("api/object-in-errors", objectInError);
                response.EnsureSuccessStatusCode();

                // return URI of the created resource.
                return response.Headers.Location;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                ErrorText = response?.ReasonPhrase;
                return null;
            }
        }

        #endregion
    }
}
