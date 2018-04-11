# Windows Forms - task 2 (Mini Paint) 

## Part 1

*   ### Main window:
    *   Starts in the center of the screen
    *   The minimum window size: 640x480
    *   Main window contains three parts: toolstrip on the top, drawing area which takes 90% of the window width regardless of the size and colors section which takes the remaining 10%
    *   Bitmap in the drawing area contains white background and always fills the entire drawing area space.
    *   Colors section on the right contains an empty group box that resizes along with the window.
    *   Toolstrip only contains the "Tools" section with a brush button.
    *   Cursor is set to cross while over the drawing area
*  ### Brush tool:
    *   When the brush tool is chosen, a user can draw lines by clicking and holding left mouse button in the drawing area.
    *   Brush color should be set to black and line thickness should be set to 1.
    *   Drawing can be stopped either by releasing the left mouse button or by pressing right mouse button while still drawing.
*  ### Colors section:
    *   The group box is now filled with all colors from the KnownColor class.
    *   Each color is represented by a 25x25 square filled with that color.
    *   Clicking on the square changes the current painting color.
    *   The content inside the group box should be scrollable.
    *   Colors section has a flow layout - the wider the section is, the more colors it can fit in a single row.
*  ### Hints:
    *   TableLayoutPanel, PictureBox, SizeChanged Event
    *   MouseDown, MouseMove, MouseUp Events, Graphics methods, Refresh method
    *   FlowLayoutPanel, AutoScroll Property, Color.FromKnownColor Method
*  ### _Note: In all doubtful and untold aspects application should behave like example app (except possible bugs)_

*   Scoring:
    *   "Main window" and "Brush tool" parts: 4 points
    *   "Colors section" part: 4 points
    *   _Note: It is not possible to obtain points for incomplete functionality_

## Part 2

* ### General:
    *   One of the supported languages must be English - the second one is unrestricted.
    *   Each section in the toolstrip is separated by a vertical separator (just like in the example app)
* ### File section:
    *   Bitmap can now be both saved and loaded using the following file extensions: .png, .bmp, .jpeg
    *   Window size should adjust to the loaded bitmap size.
* ### Tools section:
    *   Apart from the brush tool a rectangle tool and ellipse tool is present.
    *   Both of the new tools work just like in MS Paint - holding the left mouse button and moving over the drawing area displays a shape and when the left mouse button is released the shape is permanently added to the bitmap.
    *   Clicking the right mouse button during the shape creation cancels the event.
    *   Only one tool can be picked at a time.
    *   Clear tool simply clears the bitmap with white color.
    *   Line thickness can be chosen for all tools (1-small, 2-medium, 3-big)
* ### Chosen color:
    *   The tooltip also contains a small box displaying currently chosen color.
    *   Apart from that, whenever a user chooses the color from the color section on the right, chosen color is emphasized by a dashed square in negative to the chosen color
* ### Languages & localization:
    *   The application should be localizable and should support two languages.
    *   When launched, application is set to english.
    *   The toolstrip contains two buttons that - when clicked - change the current culture info. Every string (including the tooltips) is changed to another language.  
        **Warning:** this should be done using the .resx files and localization, changing Text and Title properties won't be approved.
    *   All the controls should be properly reloaded after the culture change. That includes the chosen tool, thickness, etc.
    *   The window should stay in the same position and maintain the same size.
* ### Hints:
    *   Image.Save, Image.FromFile
    *   DrawEllipse, DrawRectangle
    *   Invalidate method, DashPattern, Color.ToArgb, Color.FromArgb
    *   Form.Localizable, CultureInfo.CurrentUICulture
* ### _Note: In all doubtful and untold aspects application should behave like example app (except possible bugs)_

* ### Approximate scoring:
    *   proper file saving and loading: **2 points**
    *   rectangle, ellipse tools: **3 points**
    *   clear tool and thickness: **1 point**
    *   chosen color box in the tool strip: **1 point**
    *   dashed line around the chosen color in the colors section: **1 point**
    *   changing languages with localization: **2 points**
    *   proper application behavior after changing a language: **2 points**
    *   **Note**: To pass part 2, all of the functionality of the part 1 must be fulfilled._