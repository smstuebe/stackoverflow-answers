using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;
using Android.Views;

namespace SpinnerList
{

    public class Attendence
    {
        public string ClassId { get; set; }
        public string ClassName { get; set; }
        public string StudentName { get; set; }
        public string Attend { get; set; }
        //TODO: maybe some more
    }

    [Activity(Label = "SpinnerList", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private List<Attendence> _list;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            _list = new List<Attendence>
            {
                new Attendence {StudentName = "Horst", ClassId = "1", ClassName = "Math", Attend = "muh"},
                new Attendence {StudentName = "Peter", ClassId = "1", ClassName = "Math", Attend = "bar"},
                new Attendence {StudentName = "Hans", ClassId = "1", ClassName = "Math", Attend = "bar1"},
                new Attendence {StudentName = "Klaus", ClassId = "1", ClassName = "Math", Attend = "bar2"},
                new Attendence {StudentName = "Klaus", ClassId = "1", ClassName = "Math", Attend = "bar2"},
                new Attendence {StudentName = "Klaus", ClassId = "1", ClassName = "Math", Attend = "bar2"},
                new Attendence {StudentName = "Klaus", ClassId = "1", ClassName = "Math", Attend = "bar2"},
                new Attendence {StudentName = "Klaus", ClassId = "1", ClassName = "Math", Attend = "bar2"},
                new Attendence {StudentName = "Klaus", ClassId = "1", ClassName = "Math", Attend = "bar2"},
                new Attendence {StudentName = "Klaus", ClassId = "1", ClassName = "Math", Attend = "bar2"},
                new Attendence {StudentName = "Klaus", ClassId = "1", ClassName = "Math", Attend = "bar2"},
                new Attendence {StudentName = "Klaus", ClassId = "1", ClassName = "Math", Attend = "bar2"},
                new Attendence {StudentName = "Klaus", ClassId = "1", ClassName = "Math", Attend = "bar2"},
                new Attendence {StudentName = "Klaus", ClassId = "1", ClassName = "Math", Attend = "bar2"},
                new Attendence {StudentName = "Klaus", ClassId = "1", ClassName = "Math", Attend = "bar2"},
                new Attendence {StudentName = "Klaus", ClassId = "1", ClassName = "Math", Attend = "bar2"},
                new Attendence {StudentName = "Klaus", ClassId = "1", ClassName = "Math", Attend = "bar2"},
                new Attendence {StudentName = "Klaus", ClassId = "1", ClassName = "Math", Attend = "muh"}
            };

            var btnSvae = FindViewById<Button>(Resource.Id.btnSave);
            btnSvae.Click += BtnSvaeOnClick;

            ListView lsStu = FindViewById<ListView>(Resource.Id.listStudentAttendence);


            AttendenceViewAdapter adapter = new AttendenceViewAdapter(this, _list);
            lsStu.Adapter = adapter;
        }

        private void BtnSvaeOnClick(object sender, EventArgs eventArgs)
        {
            foreach (var attendence in _list)
            {
                System.Diagnostics.Debug.WriteLine("insert into student_attendence values ('" + attendence.ClassId + "','" + attendence.ClassName + "','" + attendence.StudentName + "','" + attendence.Attend + "');");
            }
        }
    }

    public class AttendenceViewAdapter : BaseAdapter<Attendence>
    {
        private List<Attendence> mstudents;
        private Context mcontext;
        private string[] _array;

        public AttendenceViewAdapter(Context context, List<Attendence> stud)
        {
            mstudents = stud;
            mcontext = context;
            _array = context.Resources.GetStringArray(Resource.Array.attendence_array);
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;
            Spinner spinner;
            if (view == null)
            {
                view = LayoutInflater.From(mcontext).Inflate(Resource.Layout.listview_attendence, null, false);
                spinner = view.FindViewById<Spinner>(Resource.Id.spinnerTeacherAttendence);
                var adapter = ArrayAdapter.CreateFromResource(mcontext, Resource.Array.attendence_array, Android.Resource.Layout.SimpleDropDownItem1Line);
                adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

                spinner.Focusable = false;
                spinner.FocusableInTouchMode = false;
                spinner.Clickable = true;

                spinner.ItemSelected += Spinner_ItemSelected;
                spinner.Adapter = adapter;
            }
            else
            {
                spinner = view.FindViewById<Spinner>(Resource.Id.spinnerTeacherAttendence);
            }

            // set view properties to reflect data for the given row
            TextView txtStudent = view.FindViewById<TextView>(Resource.Id.textStudentNameTeacherAttendence);
            txtStudent.Text = mstudents[position].StudentName;


            spinner.SetSelection(Array.IndexOf(_array, mstudents[position].Attend));
            spinner.Tag = position; // use this to know which student has been edited

            // return the view, populated with data, for display
            return view;
        }

        public override int Count { get { return mstudents.Count; } }

        void Spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            var abc = spinner.SelectedItemPosition;
            var position = (int)spinner.Tag;

            spinner.RequestFocusFromTouch();
            spinner.SetSelection(abc);

            mstudents[position].Attend = (string)spinner.Adapter.GetItem(abc);
        }

        public override Attendence this[int position]
        {
            get { return mstudents[position]; }
        }
    }
}

