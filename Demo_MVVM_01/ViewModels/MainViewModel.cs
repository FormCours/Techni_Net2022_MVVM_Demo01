using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_MVVM_01.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Event
        // La vue va automatiquement s'abonner a l'event.
        // Si l'event est déclanché, la vue va utiliser le getter correspondant
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Field
        private int _Count;
        private string _CountInfo;
        private string _Message;
        private string _Message2;
        #endregion

        #region Props
        public int Count
        {
            get { return _Count; }
            set 
            { 
                _Count = value;

                // Déclanchement de l'event pour signaler que "Count" a changer
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Count)));
            }
        }

        public string CountInfo
        {
            get { return _CountInfo; }
            set
            {
                _CountInfo = value;

                // Déclanchement de l'event pour signaler que "CountInfo" a changer
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CountInfo)));
            }
        }

        public string Message
        {
            get { return _Message; }
            set { _Message = value.ToUpper(); }
        }
        public string Message2
        {
            get { return _Message2; }
            set { _Message2 = value.ToUpper(); }
        }
        #endregion

        #region Ctor
        public MainViewModel()
        {
            _Count = 0;
            _CountInfo = "Le compteur est en cours";
            _Message = "";
            _Message2 = "";

            Task t = new Task(UpdateCount);
            t.Start();
        }
        #endregion

        #region Méthode
        private void UpdateCount()
        {
            while(Count < 1_000)
            {
                System.Threading.Thread.Sleep(10);

                // Il est necessaire de prévenir la vue que le compteur est modifier !
                // Pour cela, on va envoyer un event dans le setter
                Count++;
            }
            CountInfo = "Le compteur a terminé !";
        }
        #endregion
    }
}
