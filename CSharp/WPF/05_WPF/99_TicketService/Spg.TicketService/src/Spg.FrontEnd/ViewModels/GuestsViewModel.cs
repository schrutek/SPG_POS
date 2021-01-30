using Spg.Services.Dtos;
using Spg.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spg.FrontEnd.ViewModels
{
    public class GuestsViewModel : ViewModelBase
    {
        public IEnumerable<GuestDto> Guests { get; set; }

        public GuestDto CurrentGuest 
        { 
            get => _currentGuest;
            set
            {
                _currentGuest = value;
                OnPropertyChanged(nameof(CurrentGuest));
            }
        }
        private GuestDto _currentGuest;

        public GuestsViewModel() 
        {
            GetAllGuests();
        }

        private void GetAllGuests()
        {
            SetProperty("Guests", (EventService.Instance.GetAllGuests()).ToList());
        }
    }
}
