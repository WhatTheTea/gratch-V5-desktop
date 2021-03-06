
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

using Splat;

using DynamicData;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Linq;
using System.Reactive.Linq;

namespace gratch_desktop.ViewModels
{
    public class GraphViewModel : BaseViewModel, IRoutableViewModel
    {
        public string UrlPathSegment => "/graph";
        public IScreen HostScreen { get; }
        public ReadOnlyObservableCollection<AssigneesItemViewModel> Assignees; 

        [Reactive]
        public bool FlyoutIsOpen { get; set; }
        //Calendar
        [Reactive]
        public DateTime? SelectedCalendarDate { get; set; }
        public DateTime CalendarStartDate => new(DateTime.Now.Year, DateTime.Now.Month, 1);
        public DateTime CalendarEndDate => new(DateTime.Now.Year, DateTime.Now.Month,
            DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));


        public GraphViewModel(IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();

            SelectedCalendarDate = null;

            var test = this.WhenAnyValue(x => x.SelectedCalendarDate)
                .Subscribe(x =>
                {
                    if (SelectedCalendarDate != null || SelectedCalendarDate != default)
                    {
                        FlyoutIsOpen = false;
                        //SelectedCalendarDate.ObservableForProperty(x => )
                        FlyoutIsOpen = true;
                    }
                });
            var assignees = groupService.Connect()
                .Filter(x => x.Any())
                .Transform(g => new AssigneesItemViewModel(g, SelectedCalendarDate))
                .ObserveOnDispatcher()
                .Bind(out Assignees)
                .DisposeMany()
                .Subscribe();
            /*var assignees = groupService.Connect()
                .Filter(x => x.Any())
                .Transform(x => new AssigneesItemViewModel(x, SelectedCalendarDate))
                .Subscribe();*/

        }
    }
}
