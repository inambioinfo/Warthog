# Warthog
Software to compare mass spectra and determine how many similar peaks there are between them

##Versions:
v1: Original Version:

This program reads in data from the database created in BabyKingLionCub and bins the number of overlapping peaks per edit distance.
It then outputs the bins to a text file that can be used to make a histogram.  If you do not specify a path for your output file, it 
will automatically store to the Debug folder.

####To Run:
  To run this program, open a new windows command prompt and type the following command: 
  
    MzMerger.exe -r PathToWhereYourDatabaseIsStored -o PathAndNameYouWantForYourOutputFile


---
v2: MzMergerWithPeakShifting:

This program is similar to the original version, but now has been updated with the Query and Library objects.  Within the Library
class is a new set of methods which creates alternative versions of the original m/z list from the database created in 
Meerkat v3-LibraryCreator. These alternative m/z lists include the same values as before, but with the peaks that matched
to values in the precursor list for each residue shifted by the precursor m/z value for the charge of each scan. The current output 
of this program is a text file containing a column of twenty-one values corresponding to the number of pairs that had each of those
match numbers ranging zero to twenty.  

####To Run:
  To run this program, open a new windows command prompt and type the following command:
  
    MzMerger.exe -r PathToWhereYourQueryDatabaseIsStored -d PathToWhereYourLibraryDatabaseIsStored 
    -p PathToWhereYourQueryAndLibraryPairsDatabaseIsStored -o PathAndNameYouWantForYourOutputFile
