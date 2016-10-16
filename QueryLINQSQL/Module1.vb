'Joshua Pickenpaugh
'October 15th, 2016
'LINQ to SQL Project UNIT 3

Imports System.Linq
Imports System.Collections.Generic
Imports System.Data.Linq
Imports System.Data.Linq.Mapping

Module Module1

    Sub Main()

        Dim cse_dept_typed_in As New DataClasses1DataContext()

        Console.WriteLine("Make your selection: " & vbNewLine)
        Console.WriteLine("1: LINQ to SQL Select query")
        Console.WriteLine("2: LINQ to SQL Insert query")
        Console.WriteLine("3: LINQ to SQL Update query")
        Console.WriteLine("4: LINQ to SQL Delete query")
        Console.WriteLine("5: Exit the project." & vbNewLine)

        Dim input As String = Console.ReadLine()

        Select Case (input)
            Case "1"
                LINQSelect(cse_dept_typed_in)
            Case "2"
                LINQInsert(cse_dept_typed_in)
            Case "3"
                LINQUpdate(cse_dept_typed_in)
            Case "4"
                LINQDelete(cse_dept_typed_in)
            Case "5"
                Exit Sub
            Case Else
                Console.WriteLine("Invalid input value.")

        End Select

        Console.WriteLine(vbNewLine & "Press Enter to continue...")
        Console.ReadKey()
    End Sub

    Private Sub LINQSelect(ByRef db As DataClasses1DataContext)

        Dim faculty = From fi In db.Faculties
                      Where fi.faculty_id = "B78880"
                      Select fi

        For Each f In faculty
            Console.WriteLine("{0},{1},{2},{3},{4},{5}", f.faculty_name, f.title, f.office, f.phone, f.college, f.email)
        Next
    End Sub

    Private Sub LINQInsert(ByRef db As DataClasses1DataContext)

        'Create the new Faculty object:
        Dim newFaculty As New Faculty()
        newFaculty.faculty_id = "D19886"
        newFaculty.faculty_name = "David Winner"
        newFaculty.title = "Department Chair"
        newFaculty.office = "MTC-333"
        newFaculty.phone = "750-330-1255"
        newFaculty.college = "University of Hawaii"
        newFaculty.email = "dwinner@college.edu"

        'Add the faculty to the Faculty table:
        db.Faculties.InsertOnSubmit(newFaculty)
        db.SubmitChanges()

        'Query back the new inserted faculty member:
        Dim fi = db.Faculties.Where(Function(f) f.faculty_id = "D19886").First()
        Console.WriteLine("{0},{1},{2},{3},{4},{5}", fi.faculty_name, fi.title, fi.office, fi.phone, fi.college, fi.email)

        'Reset the database by deleting the new inserted factulty:
        db.Faculties.DeleteOnSubmit(newFaculty)
        db.SubmitChanges()

    End Sub

    Private Sub LINQUpdate(ByRef db As DataClasses1DataContext)

        Dim fi = db.Faculties.Where(Function(f) f.faculty_id = "B78880").First()

        'Display the existing faculty information:
        Console.WriteLine("Before the faculty table updated..." & vbNewLine)
        Console.WriteLine("{0},{1},{2},{3},{4},{5}", fi.faculty_name, fi.title, fi.office, fi.phone, fi.college, fi.email)

        'Update the faculty name:
        fi.faculty_name = "New Faculty"
        db.SubmitChanges()
        Console.WriteLine(vbNewLine & "After the Faculty table is updated..." & vbNewLine)
        Console.WriteLine("{0},{1},{2},{3},{4},{5}", fi.faculty_name, fi.title, fi.office, fi.phone, fi.college, fi.email)

        'Recover the original column for the Faculty table:
        fi.faculty_name = "Ying Bai"
        db.SubmitChanges()

    End Sub

    Private Sub LINQDelete(ByRef db As DataClasses1DataContext)

        Dim faculty As Faculty = (From fi In db.Faculties
                                  Where fi.faculty_id = "B78880"
                                  Select fi).Single()

        db.Faculties.DeleteOnSubmit(faculty)
        db.SubmitChanges()

        'Try to retrieve back and display the deleted faculty information:
        Dim delfaculty As Faculty = (From fi In db.Faculties Where fi.faculty_id = "B78880" Select fi).SingleOrDefault()

        Console.WriteLine("Faculty {0} found.", delfaculty)
    End Sub
End Module
