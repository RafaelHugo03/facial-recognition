using System;
using Amazon.Rekognition;
using Amazon.Runtime;
using Amazon.S3;

namespace FacialRecognition.Factory
{
    public static class AwsFactory
    {
        public static AmazonS3Client GetAmazonS3Client()
        {
            var credentials = new BasicAWSCredentials("AKIAZIHYABNXMQTANIE2", "t/+Snuwt5vAR0CWDcHcMI8c84neQH1ETU/pShjT1");

            return new AmazonS3Client(credentials, Amazon.RegionEndpoint.USEast2);
        }

        public static AmazonRekognitionClient GetAmazonRekognitionClient()
        {
            var credentials = new BasicAWSCredentials("AKIAZIHYABNXMQTANIE2", "t/+Snuwt5vAR0CWDcHcMI8c84neQH1ETU/pShjT1");

            return new AmazonRekognitionClient(credentials, Amazon.RegionEndpoint.USEast2);
        }
    }
}
