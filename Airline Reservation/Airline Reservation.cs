//Maria Waleska Marinho de Oliveira 
//ID:8751749
//email: mmarinhodeolive1749@conestogac.on.ca

namespace Airline_Reservation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            //Clear the Erro msg lbl and also the statusMsg:

            lblErroMassage.Text = "";
            StatusLabelMain.Text = _emptyPassagerMessage;
        }


        private void btnShowAll_Click(object sender, EventArgs e)
        {
            //Var that I will use to fill the rtbBox;
            string AllSeats = "";
            
            //loop to display all the passeger names
            for (int i = 0; i < numOfColumns; i++)
            {
                for (int q = 0; q < numOfRows; q++)
                {
                    AllSeats += $"{_passegerName[q,i]} \n";
                }  
            }
            //Fill the RichTextBox
            rtbShowSeat.Text = AllSeats;

        }



        private void btnBook_Click(object sender, EventArgs e)
        {
            // clean all the msg 
            StatusLabelMain.Text = "";
            // string that will hold the erro msg to be displayed 
            string errMsg = "";
            // I creat 2 ints to store the user input seats
            int selectedRowIndex = listBoxRows.SelectedIndex;
            int selectedColumsIndex = listBoxColumns.SelectedIndex;



            if (psgCouter == _passegerName.Length)
            {
                lblErroMassage.Text = _fullPassagerMessage;

                // loop tto add people in the waiting room 
                for (int i = 0; i < _waitingList.Length; i++)
                {
                    //check if the waiting room is empty 
                    if (string.IsNullOrEmpty(_waitingList[i]))
                    {
                        // assign the user input inside the array 
                        _waitingList[i] = txtName.Text;
                        lblErroMassage.Text = "";
                        if (txtName.Text != "")
                        {
                            StatusLabelMain.Text = "You has been added to the waiting list";
                            txtName.Text = "";
                            
                        }else
                        {
                            lblErroMassage.Text = "Please input  a name";
                            return;
                        }

                        return;

                    }
                }
                lblErroMassage.Text = "The waiting list is full";

            }


              //Check is the user  did not put a empty string 
              if (string.IsNullOrEmpty(txtName.Text) )
              {
               lblErroMassage.Text = errMsg += "A Name has not been inputed - Please input the passeger Name! \n";
                
              }
            //Check is the user  did not choose a row
            if (selectedRowIndex == -1 )
            {
                lblErroMassage.Text= errMsg += "A row is not selected - Please select one! \n";
                
            }
            //Check is the user  did not choose a colum
            if (selectedColumsIndex == -1 )
            {
                lblErroMassage.Text =  errMsg += "A column is not selected - Please select one!\n";
                
            }

            // If the user input a name and a row and colum, and if the bool array is false means that the seat is available
            // i will display the Name and the seat in the lbl.


            if (errMsg == "" && availableSeats[selectedRowIndex, selectedColumsIndex] == false)
            {
                lblErroMassage.Text = $" Passenger Name: {txtName.Text} your seat is {listBoxRows.Items[selectedRowIndex]} {listBoxColumns.Items[selectedColumsIndex]}";
            }
            else
            {
                lblErroMassage.Text = errMsg;
                return;
            }
            // check  if the seat is available using the bool array, in case it is true means that there is someone there.
            if (availableSeats[selectedRowIndex, selectedColumsIndex] == true)
            {
                lblErroMassage.Text = " This seat is already selected please chose another one";
                return;
            }
            // store the passager in the string the format  "A1- NAME"
            string newPassager = seats[selectedRowIndex, selectedColumsIndex] + "-" + txtName.Text.Trim();

            // check if the passager is not empty something has been inputed
            if (newPassager != "" && txtName.Text != "")
            {
                //assign it into the passages array at the current count and then  bump up the count.
                _passegerName[selectedRowIndex, selectedColumsIndex] = newPassager;
                psgCouter++;
                txtName.Text = "";
               
                //change the bool array to true so we can change the seat to occupied
                availableSeats[selectedRowIndex, selectedColumsIndex] = true;
               
            }

            //set the status label
            if (psgCouter >= 1)
            {
                StatusLabelMain.Text = $"Number of passagens: {psgCouter} ";
            }
            else
            {
                StatusLabelMain.Text = _emptyPassagerMessage;
            }
            // check if the airplane is full

        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtStatus.Text = "";
            //Check is the user have not input the seat 

            if (listBoxColumns.SelectedIndex == -1 || listBoxRows.SelectedIndex == -1)
            {
                lblErroMassage.Text = "please choose a seat to be deleted";
                _emptyPassagerMessage = "";
                return;
            }

            // ask if the user are sure he wantes to delite it 
            DialogResult response = MessageBox.Show("Are you sure you want to cancel?", "Confirmation", MessageBoxButtons.YesNo);
            if (response == DialogResult.Yes)
            {
                // if the user are sure he wants to delite it the bool array that represent the seats became false with means the it is available
                availableSeats[listBoxRows.SelectedIndex, listBoxColumns.SelectedIndex] = false;
                
                // display a msg sayng that the seat has been delited
                MessageBox.Show($" Seat : { seats[listBoxRows.SelectedIndex, listBoxColumns.SelectedIndex]} has been canceled successfully ");
               
                // now i will delite the name of the passagen form the array in the index thta the user choose related to the seats.
                _passegerName[listBoxRows.SelectedIndex, listBoxColumns.SelectedIndex] = "";
                // i will decrease the couny of passagens in the plane
                psgCouter--;

                // set the status with the curent number.
                StatusLabelMain.Text = $"Number of passagens: {psgCouter}";
                //clean the lbl 
                lblErroMassage.Text = "";

                // Create a new string = A1 + - + (firstIndex) what is the first person in the waiting room  and
                // this new person will be in the place of the deleted person
                // 
                _passegerName[listBoxRows.SelectedIndex, listBoxColumns.SelectedIndex] = seats [listBoxRows.SelectedIndex, listBoxColumns.SelectedIndex]+ "-" +_waitingList[0];

                StatusLabelMain.Text = "Seat has been callced";

                // loop to replace the second person in the waiting list to to first place.

                for (int i = 1; i < _waitingList.Length; i++)
                {
                    // check if the string is valid in the index of the loop 

                    if (!string.IsNullOrEmpty(_waitingList[i]))
                    {
                        // if so we will assign to the waiting list update of the list of the people.
                        sortingWaithing[i - 1] = _waitingList[i];
                    }
                    else
                    {
                        break;
                    }
                }

                // put the value of the list of passager in the sorting list into the waiting list the value updated 

                for (int i = 0; i < _waitingList.Length; i++)
                {
                    _waitingList[i] = sortingWaithing[i];
                }
               
                // clean the sorting list 
                for (int i = 0; i < _waitingList.Length; i++)
                {
                    sortingWaithing[i] = "";
                }

            }
            else if (response == DialogResult.No)
            {
                return;
            }

        }


        private void btnAddToWaitList_Click(object sender, EventArgs e)
        {
            //check input 
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                // check is the plane is full 

                if (psgCouter < 15)
                {
                    lblErroMassage.Text = "Seats are available";
                    return;
                }
                else
                {
                    // loop tto add people in the waiting room 
                    for (int i = 0; i < _waitingList.Length; i++)
                    {
                        //check if the waiting room is empty 
                        if (string.IsNullOrEmpty(_waitingList[i]))
                        {
                            // assign the user input inside the array 
                            _waitingList[i] = txtName.Text;
                            lblErroMassage.Text = "";
                            StatusLabelMain.Text = "You has been added to the waiting list";
                            txtName.Text = "";
                            return;
                        }


                    }
                }

                lblErroMassage.Text = "The waiting list is full";
                StatusLabelMain.Text = "";
            }
        }


        private void btnShowWaitingList_Click(object sender, EventArgs e)
        {
            // use a loop to display the passager names

            StatusLabelMain.Text="";
            rtbShowWaitingList.Text = "";

            foreach (string name in _waitingList)
            {
                rtbShowWaitingList.Text += name + "\n";

            }
        }

        private void btnStatus_Click(object sender, EventArgs e)
        {
            lblErroMassage.Text = "";
            //check user entre 
            if (txtName.Text == "" && listBoxRows.SelectedIndex == -1 && listBoxColumns.SelectedIndex == -1)
            {
                lblErroMassage.Text = "Please first choose a seat";
                return;

            }
            //check is there is availabel seats 
            if (availableSeats[listBoxRows.SelectedIndex, listBoxColumns.SelectedIndex] == false)
            {
                txtStatus.Text = " The seat is empty!";
            }
            else
            {
                for (int i = 0; i < psgCouter; i++)
                {
                    //check if the passager contains the numer of the seat ( A1-NAME )
                    if (_passegerName[listBoxRows.SelectedIndex, listBoxColumns.SelectedIndex].Contains(seats[listBoxRows.SelectedIndex, listBoxColumns.SelectedIndex]))
                    {
                        // we will split the string array until get after the - what is [1] sign where is the name of the passager.
                        string[] psgName = _passegerName[listBoxRows.SelectedIndex, listBoxColumns.SelectedIndex].Split('-');
                        string apsgNameString = psgName[1];
                        txtStatus.Text = $"This seat is occupited for: {apsgNameString}";
                    }
                }
            }

        }


        private void btnFillAll_Click(object sender, EventArgs e)
        {
          // Nested for loop to fill out all the seats with the name Bob also use the bool array to make it unavailable 

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    availableSeats[i, j] = true;
                    _passegerName[i, j] = seats[i,j] + " - Bob"; // A1 - BOB
                }
            }
            // set the plane as full
            psgCouter = 15;
            lblErroMassage.Text = "";
            StatusLabelMain.Text = _fullPassagerMessage;

        }

        // defult msgs
        private static string _emptyPassagerMessage = " There is not any passenger in this plane";
        private static string _fullPassagerMessage = " The airplane is full ";
        //counter var 
        private int psgCouter = 0;
        //array for the waiting list
        private string[] _waitingList = new string[10] { "", "", "", "", "", "", "", "", "", "" };
        //array to change the index of the passagers in the waitinh room 
        private string [] sortingWaithing = new string[10] { "", "", "", "", "", "", "", "", "", "" };
       
        const int numOfRows = 5;
        const int numOfColumns = 3;
        // 2d Array to strore the passagens name;
        private string[,] _passegerName = new string[numOfRows, numOfColumns]
        {
             {"","","" },
             {"","","" },
             {"","","" },
             {"","","" },
             {"","","" }
        };

        // 2d Array to strore the seats;
        string[,] seats = new string[numOfRows, numOfColumns]
           {
             {"A1","A2","A3" },
             {"B1","B2","B3" },
             {"C1","C2","C3" },
             {"D1","D2","D3" },
             {"E1","E2","E3" }
           };

        // 2d Array to strore to set the seats status;
        private bool[,] availableSeats = new bool[numOfRows, numOfColumns];       

    }
}