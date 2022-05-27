#Compifan

A compiler created for a programming language made by myself. This was part of a Six Semester proyect. 
The language has lexic, sintactics and semantic rules, too it traduces my language to arduino. Cause the language was maded to control a fan connected to an arduino by serial port.

Languages rules:
1. All the vars are declared out of the initial method called "Proceso".
2. Can't exists functions out of the "Proceso" method.
3. Only can exist a method called "Proceso".
4. The fan must be on before give it instructions.
5. The "Proceso" method and the conditions must have open and close delimiters, containig the instructions to run.
6. The comments can go anywhere.
7. Only exists integeres datatypes.

##Program body

!!Im_a_comment
int pin1=10;  
int pin2=9;
int a=6;			-> var declaration
Proceso ()  		-> Principal process loop
<  			->Open delimiter
Encender (pin1)	->Instructions and conditions
Velocidad (pin1:5)
Giro.Izquierda (pin1,pin2)
Retardo (5)			
si (a>5) 			->Condition expression
<
Giro.Derecha(pin2,pin1)
Retardo (10)
a-- 			->Subtract operator
>
>


<table>
  <theader>Token table</theader>
  <thead>
     <th>No.</th>
     <th>Token</th>
     <th>Category</th>
     <th>Function</th>
  </thead>
  <tbody>
    <tr>
      <td>1</td>
      <td>int</td>
      <td>Reserved word</td>
      <td>Defines the datatype (integer) of the posterior assign expression</td>
    </tr>
    <tr>
      <td>2</td>
      <td>x=10</td>
      <td>Assign expression</td>
      <td>Assign the value to a var defined by the user</td>
    </tr>
    <tr>
      <td>3</td>
      <td>Proceso</td>
      <td>Reserved word</td>
      <td>Default method that defines the process to run between the delimitators</td>
    </tr>
    <tr>
      <td>4</td>
      <td>()</td>
      <td>Instruction parameters</td>
      <td>Defines the parameters that a functions is receiving</td>
    </tr>
    <tr>
      <td>5</td>
      <td><</td>
      <td>Open delimiter</td>
      <td>Defines that start of a block code to methods like "Proceso" and the "si" conditions</td>
    </tr>
    <tr>
      <td>6</td>
      <td>Encender</td>
      <td>Reserved word</td>
      <td>Turns on the fan reciving a parameter</td>
    </tr>
    <tr>
      <td></td>
      <td>(1)</td>
      <td>Instuction parameters</td>
      <td>Receives the parameters of the "Encender" instruction</td>
    </tr>
    <tr>
      <td>7</td>
      <td>Velocidad</td>
      <td>Reserved word</td>
      <td>Defines the fan velocity</td>
    </tr>
    <tr>
      <td></td>
      <td>(1:10)</td>
      <td>"Velocidad" parameters</td>
      <td>Receives the pin where the fan is connected to and the velocity of the fan, splited by a ":"</td>
    </tr>
     <tr>
      <td>8</td>
      <td>Retardo</td>
      <td>Reserved word</td>
      <td>Defines an await time before executes the next code</td>
    </tr>
    <tr>
      <td>9</td>
      <td>(5)</td>
      <td>Instruction parameters</td>
      <td>Receives a parameter which is the wait time in seconds</td>
    </tr>
     <tr>
      <td>10</td>
      <td>Giro.Derecha</td>
      <td>Reserved word</td>
      <td>Defines the fan turn direction to right</td>
    </tr>
    <tr>
      <td>11</td>
      <td>(1,2)</td>
      <td>Fan turn direction parameters</td>
      <td>Receives the connected fan pin to turn on and turn off to change the turn direction</td>
    </tr>
      <tr>
      <td>12</td>
      <td>Giro.Izquierda</td>
      <td>Reserved word</td>
      <td>Defines the fan turn direction to left</td>
    </tr>
    <tr>
      <td>13</td>
      <td>(2,1)</td>
      <td>Fan turn direction parameters</td>
      <td>Receives the connected fan pin to turn on and turn off to change the turn direction</td>
    </tr>
    <tr>
      <td>14</td>
      <td>si</td>
      <td>Reserved word</td>
      <td>Evaluates if a conditon is right or not to execute a code block</td>
    </tr>
     <tr>
      <td>15</td>
      <td>(x<10)</td>
      <td>Condition expression</td>
      <td>Receives a evaluation based on a declared user var or a number</td>
    </tr>
     <tr>
      <td>16</td>
      <td>x++/x--</td>
      <td>Add and substract operatores</td>
      <td>Add or substract one to a defined var</td>
    </tr>
    <tr>
      <td>17</td>
      <td>></td>
      <td>Close delimiter</td>
      <td>Defines the end of a code block</td>
    </tr>
    
  </tbody>
</table>

