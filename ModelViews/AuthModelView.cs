﻿using System;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MedicalRefbook2_0;

namespace MedicalRefbook2_0.ModelViews
{
    public class AuthModelView : INotifyPropertyChanged
    {
        private bool _IsAuth = false;

        public string Username { get; set; } = "postgres";
        public string Password { get; set; } = "root";
        public Models.AuthModel AuthModelP { get; set; }
        public ICommand AuthCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }
        public bool IsAuth { get { return _IsAuth; } set { _IsAuth = value; NotifyPropertyChanged(); } }
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string PropertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public AuthModelView()
        {
            AuthModelP = new Models.AuthModel();
            AuthCommand = new DelegateCommand(CheckUser, CanClickLogin);
            ExitCommand = new DelegateCommand(ExitUser);
        }

        private void ExitUser(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanClickLogin(object arg)
        {
            
            if(Username.Length == 0 || Password.Length == 0)
            {
                return false;
            }
            return true;
        }

        private void CheckUser(object obj)
        {
            obj = new string[] { Username, Password };
            IsAuth = AuthModelP.CheckUser(obj);
            if (IsAuth == true)
            {
                Views.MainWindow mainView = new Views.MainWindow();
                mainView.Show();
                ((App)Application.Current).AuthV.Close();
            }
        }
    }
}