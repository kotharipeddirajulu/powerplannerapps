﻿using PowerPlannerAppDataLibrary.ViewItems;
using PowerPlannerAppDataLibrary.ViewItemsGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BareMvvm.Core.ViewModels;
using PowerPlannerAppDataLibrary.ViewItems.BaseViewItems;
using PowerPlannerAppDataLibrary.ViewModels.MainWindow.MainScreen.Homework;
using ToolsPortable;
using PowerPlannerAppDataLibrary.ViewModels.MainWindow.MainScreen.Class;
using PowerPlannerAppDataLibrary.ViewModels.MainWindow.MainScreen.Schedule;
using PowerPlannerAppDataLibrary.ViewModels.MainWindow.MainScreen.Holiday;

namespace PowerPlannerAppDataLibrary.ViewModels.MainWindow.MainScreen.Day
{
    public class DayViewModel : BaseMainScreenViewModelChild
    {
        public SemesterItemsViewGroup SemesterItemsViewGroup { get; private set; }

        public DateTime Today { get; private set; } = DateTime.Today;

        public DayViewModel(BaseViewModel parent, Guid localAccountId, ViewItemSemester semester) : base(parent)
        {
            Initialize(localAccountId, semester);
        }

        private void Initialize(Guid localAccountId, ViewItemSemester semester)
        {
            SemesterItemsViewGroup = SemesterItemsViewGroup.Load(localAccountId, semester);
        }

        public void ShowItem(BaseViewItemHomeworkExam item)
        {
            MainScreenViewModel.ShowItem(item);
        }

        private DateTime _currentDate = NavigationManager.GetSelectedDate();
        public DateTime CurrentDate
        {
            get { return _currentDate; }
            set { SetProperty(ref _currentDate, value.Date, nameof(CurrentDate)); NavigationManager.SetSelectedDate(value); NavigationManager.SetDisplayMonth(value); }
        }

        public void AddHomework()
        {
            AddItem(AddHomeworkViewModel.ItemType.Homework);
        }

        public void AddExam()
        {
            AddItem(AddHomeworkViewModel.ItemType.Exam);
        }

        public void AddHoliday()
        {
            MainScreenViewModel.ShowPopup(AddHolidayViewModel.CreateForAdd(MainScreenViewModel, new AddHolidayViewModel.AddParameter()
            {
                SemesterIdentifier = MainScreenViewModel.CurrentSemesterId,
                StartDate = CurrentDate,
                EndDate = CurrentDate
            }));
        }

        private void AddItem(AddHomeworkViewModel.ItemType type)
        {
            MainScreenViewModel.ShowPopup(AddHomeworkViewModel.CreateForAdd(MainScreenViewModel, new AddHomeworkViewModel.AddParameter()
            {
                SemesterIdentifier = MainScreenViewModel.CurrentSemesterId,
                Classes = MainScreenViewModel.Classes,
                SelectedClass = null,
                Type = type,
                DueDate = CurrentDate
            }));
        }

        public void ViewClass(ViewItemClass c)
        {
            MainScreenViewModel.ViewClass(c);
        }

        public void ViewSchedule()
        {
            MainScreenViewModel.Navigate(new ScheduleViewModel(MainScreenViewModel));
        }

        public void ViewHoliday(ViewItemHoliday h)
        {
            MainScreenViewModel.ViewHoliday(h);
        }
    }
}
