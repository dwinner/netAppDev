﻿using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUIDemo.Model;

namespace ReactiveUIDemo.ViewModel
{
   public class ItemsViewModel : ViewModelBase
   {
      private readonly ObservableAsPropertyHelper<bool> _canAdd;
      private Todo _selectedTodo;

      /// <summary>
      ///    Reactive List https://reactiveui.net/docs/handbook/collections/reactive-list
      /// </summary>
      private ReactiveList<Todo> _todos;

      private string _todoTitl;

      public ItemsViewModel(IScreen hostScreen = null) : base(hostScreen)
      {
         this.WhenAnyValue(x => x.TodoTitle,
            title =>
               !string.IsNullOrEmpty(title)).ToProperty(this, x => x.CanAdd, out _canAdd);

         AddCommand = ReactiveCommand.CreateFromTask(() =>
         {
            Todos.Add(new Todo {Title = TodoTitle});
            TodoTitle = string.Empty;
            return Task.CompletedTask;
         }, this.WhenAnyValue(x => x.CanAdd, canAdd => canAdd && canAdd));

         //Dont forget to set ChangeTrackingEnabled to true.
         Todos = new ReactiveList<Todo> {ChangeTrackingEnabled = true};

         Todos.Add(new Todo {IsDone = false, Title = "Go to Sleep"});
         Todos.Add(new Todo {IsDone = false, Title = "Go get some dinner"});
         Todos.Add(new Todo {IsDone = false, Title = "Watch GOT"});
         Todos.Add(new Todo {IsDone = false, Title = "Code code and code!!!!"});

         // Lets detect when ever a todo Item is marked as done 
         // IF it is, it is sent to the bottom of the list
         // Else nothing happens
         Todos.ItemChanged.Where(x => x.PropertyName == "IsDone" && x.Sender.IsDone)
            .Select(x => x.Sender)
            .Subscribe(x =>
            {
               if (x.IsDone)
               {
                  Todos.Remove(x);
                  Todos.Add(x);
               }
            });
      }

      public ReactiveList<Todo> Todos
      {
         get => _todos;
         set => this.RaiseAndSetIfChanged(ref _todos, value);
      }

      public Todo SelectedTodo
      {
         get => _selectedTodo;
         set => this.RaiseAndSetIfChanged(ref _selectedTodo, value);
      }

      public bool CanAdd => _canAdd?.Value ?? false;

      public string TodoTitle
      {
         get => _todoTitl;
         set => this.RaiseAndSetIfChanged(ref _todoTitl, value);
      }

      public ReactiveCommand AddCommand { get; }
   }
}