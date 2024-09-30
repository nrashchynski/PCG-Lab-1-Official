# **Color Converter**

## Project Overview
This application is designed to allow users to interactively select and modify colors while displaying their components in three different color models: RGB, LAB, and CMYK. The primary goal is to facilitate seamless color transformations between these models, ensuring automatic recalculations across all models when any component is changed.

## Color Models
The application utilizes the following color models:
1. RGB (Red, Green, Blue):
RGB is one of the most widely used color models, especially in digital displays. In this model, colors are created by combining varying intensities of red, green, and blue light. The intensity of each component ranges from 0 to 255. The RGB model is additive, meaning that higher values result in lighter colors, and combining all components at their maximum values produces white.
2. LAB (Lightness, A, B):
The LAB color model is designed to be more perceptually uniform than RGB or CMYK, meaning that changes in the LAB values should correspond more closely to human vision's perception of color differences. It consists of three components: Lightness (L), which represents the lightness of the color, and two color-opponent dimensions, A (green to red) and B (blue to yellow). LAB is often used in color correction and device-independent color spaces.
3. CMYK (Cyan, Magenta, Yellow, Key/Black):
CMYK is the standard color model used in color printing. It is a subtractive color model where colors are produced by combining different percentages of cyan, magenta, yellow, and black ink. Unlike RGB, where light is added to produce colors, CMYK subtracts light, so adding all colors together creates black, and removing them all results in white (the color of the paper).

## Key Features
1. Interactive Color Selection:
Users can input colors using either RGB text boxes or adjust color sliders for more granular control.
A color panel (colorDisplayPanel) displays the currently selected color based on the RGB values.
2. Automatic Conversion:
Any change in the RGB values automatically converts the color to both LAB and CMYK models.
The converted values are displayed in respective text boxes for LAB (L, A, B) and CMYK (C, M, Y, K).
3. Slider Synchronization:
When RGB sliders are adjusted, the values in the RGB text boxes are updated, and the color is immediately reflected in the colorDisplayPanel.
Changes in any color model (RGB, LAB, or CMYK) are reflected across the entire interface in real-time.
4. Error Handling:
In cases where the converted values exceed the permissible ranges (e.g., RGB values out of the 0-255 range), non-intrusive warnings are displayed, indicating rounding or clamping adjustments.

## How It Works
1. RGB Input:
Users can manually input values in the RTextBox, GTextBox, and BTextBox or use the sliders (RTrackBar, GTrackBar, BTrackBar) to adjust the RGB values.
The RGB values are converted to LAB and CMYK models using the following transformation pipeline:
RGB to LAB: Using standard XYZ conversion, followed by conversion to LAB.
RGB to CMYK: Using a mathematical formula based on RGB component ratios.
2. LAB and CMYK Display:
The application automatically updates the LAB and CMYK text boxes (lTextBox, aTextBox, b_TextBox for LAB; cTextBox, mTextBox, yTextBox, kTextBox for CMYK) when any changes are made to the RGB values.
3. Real-Time Color Display:
A panel (colorDisplayPanel) dynamically updates to show the color that corresponds to the current RGB values.
4. Reverse Transformation:
Colors can also be inputted using LAB and CMYK models. The application converts them back to RGB to maintain consistency and synchronize with the sliders.

## Installation and Usage
1. Prerequisites:
The application runs on Windows and requires .NET Framework compatible with Windows Forms.
2. Running the Application:
After downloading the executable file, run it on a Windows machine.
The interface allows direct interaction with text boxes and sliders for color manipulation.
3. Source Code:
The source code is available on GitHub. Clone or download the repository to access the full implementation.

## Requirements
1. Input Methods: The application provides three ways to input colors:
Manual input into RGB, LAB, and CMYK text boxes.
Adjustment via RGB sliders.
Color selection from a palette (optional extension).
2. Color Models: The application must handle color conversions between the following models:
RGB ↔ LAB ↔ CMYK.

## Future Improvements
Add support for color palettes and advanced editing similar to graphics editors.
Implement additional color models such as HSV, HLS, or XYZ.
Extend error handling to provide more detailed feedback when color values fall outside of acceptable ranges.

##Contributing
If you'd like to contribute to the project:
1.Fork the repository on GitHub.
2.Make your changes in a new branch.
3.Submit a pull request with a detailed description of your changes.
