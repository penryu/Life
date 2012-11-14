Author: Tim Hammerquist
Course: CIT 134

Project: Assignment 9 - Conway's Game of Life

Solution Notes
--------------

Here are the things I finished:

 *) use of the WPF Canvas control, as WinForms appears to lack one
    *) saw note that WinForms Panel should suffice,
	   but had already fallen down this rabbit hole
 *) randomized initial state (optional)
    *) each cell has an independent chance of life in the initial state
	*) the chance of life in an initial state is user-configurable by textbox
    *) the presence of random init state is user-configurable by checkbox
 *) initial sprite patterns
    *) 11 patterns are provided in code
	*) patterns implemented as subclasses of common Pattern superclass
	*) instances are created on window creation and stored polymorphically
	   in a List<Pattern>
	*) this list is used to populate the pattern dropbox at run-time;
	   this drastically simplifies adding new patterns to the form	
    *) manual placement of patterns on the grid is not currently implement;
       however, all business logic supports this. all that remains is to
	   further clutter the already messy UI

The BIGGEST frustration I had with this was how to automate the progression
of the game. I don't know if this was any more or less difficult in WPF than
in WinForms, as I have no idea how it would be done in winforms.

After chasing the Thread/Task/BackgroundWorker carrot for too many hours,
I finally found an article that discussed Timers. But again, this was specific
to WinForms; however, the keyword allowed me to find the DispatcherTimer
class, offering the same functionality in the Dispatcher framework.

There are some places where performance could be improved. Given more time,
I also would have implemented loading of Patterns from *.LIF files described
here: http://psoup.math.wisc.edu/mcell/ca_files_formats.html#Life%201.05
This would have simplified the Pattern class hierarchy even further by
allowing me to abstract even more grid[] access into the base class.

As a side note, I did violate the "one class per file" rule for this,
but I hope you'll agree that these subclasses really don't warrant their own
files.  Also, another decent example of polymorphism in generic collections.

Thank you again for an interesting and challenging assignment.
Hope all is well with your home.