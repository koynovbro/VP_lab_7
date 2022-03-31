using Lab_7.Models;
using ReactiveUI;

namespace Lab_7.ViewModels
{
    internal class AddMenuViewModel : ViewModelBase
    {
        public AddMenuViewModel()
        {
            newStudent = new Students("Новый студент", new int[3] { 0, 0, 0 });
        }

        Students newStudent;
        public Students NewStudent
        {

            get
            {
                return newStudent;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref newStudent, value);
            }
        }
    }
}
