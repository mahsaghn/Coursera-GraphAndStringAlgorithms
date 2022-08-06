using Microsoft.VisualStudio.TestTools.UnitTesting;
using A6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A6.Tests
{
    [TestClass()]
    public class Q2ReconstructStringFromBWTTests
    {
        [TestMethod()]
        public void SolveTest()
        {
            string result = Q2ReconstructStringFromBWT.Solve("AAT$");
            string result1 = Q2ReconstructStringFromBWT.Solve("ATTAT$TA");
            string result2 = Q2ReconstructStringFromBWT.Solve("AAAATTAT$TA");
            string result3 = Q2ReconstructStringFromBWT.Solve("AAAAAT$TTA");
            string result4 = Q2ReconstructStringFromBWT.Solve("TTTTTT$GGGGGG");
            string result5 = Q2ReconstructStringFromBWT.Solve("TTTTTGGGGTTTTTTTT$");
            string result6 = Q2ReconstructStringFromBWT.Solve("C$AAAAAACCGGGGGGTTTTTTA");
            string result7 = Q2ReconstructStringFromBWT.Solve("G$TGAGCTTGCTAGGGGCCCAAC");
            string result8 = Q2ReconstructStringFromBWT.Solve("GTC$CCCCCTTTTTTTCTTC");
            string result9 = Q2ReconstructStringFromBWT.Solve("CCATATTTAAAGGAAGCAGCGCACAGTTAGAACTTGTCAGTCGGGCAAAGTG$TTGCCTCATTAAAGTGTCAGACTAGTACTCGGCGCAG");
            string result10 = Q2ReconstructStringFromBWT.Solve("CCCGATAAATTGTGTAAAGAAGCTTCGGATTTTTATACCTAATGGCGCCAATCCGCCTGCTCGGACAAGCGGCAGGGTGGGCCTTGAAGAAATGGGCAGTAACCTATTTAATTACGACTTATATCATGTTTCATCCATTGCAACTGTTGGGGCGACCAAAATTCAAAGCAAGGTTTACGGTAGCGG$GATTGATGGCCTTCTCCAACAAGGAACTTTAATCTACTGAGAGTGGATTCAGAACTGCCTACCGACTATCCGCTTAGGCGCCCCATTGTACGTATTGCTACCGGCGATTCTGAACAACACCATGGGGTCC");
            Assert.IsTrue(result == "TAA$");
            Assert.IsTrue(result1 == "TAATTTA$");
            Assert.IsTrue(result2 == "TAATTTAAAA$");
            Assert.IsTrue(result3 == "ATTTAAAAA$");
            Assert.IsTrue(result4 == "GTGTGTGTGTGT$");
            Assert.IsTrue(result5 == "TTTTTTTTTGTGTGTGT$");
            Assert.IsTrue(result6 == "AAAAAAATTTTTTGGGGGGCCC$");
            Assert.IsTrue(result7 == "AATCGGGCTAGATCGGGCCCTG$");
            Assert.IsTrue(result8 == "CCCTTTTCCCCCTTTTTTG$");
            Assert.IsTrue(result9 == "GCAGACAGTAACGCTGTTAAAGAGACAAAAATAACAGTGGTGGTTGCGGATGAGTCCTTCCATATTCTAAATCCGCTGGAGGCTGCCTC$");
            Assert.IsTrue(result10 == "GCAGACAGTAACGCTGTTAAAGAGACAAAAATAACAGTGGTGGTTGCGGATGAGTCCTTCCATATTCTAAATCCGCTGGAGGCTGCCTCAGAAAAGTGATGTAGATAGGCTAGGTGAACGAAGTTGTCCGTCATTATTGAACTCCAGCAGGCAGACAGGCACAAAAGAGCTCGGAGTACCCTACCCTGGTTGATACGATCGGTGGATCTCGGATTACCTCCTTCTACTCAGGGGCTGGACTTTGATAACTTATGCTCTCGGCAGATTACATTCCATACACACTACCTACACGTCGACTTTGTTGTCTACTTCTCGC$");
        }
    }
}