using Amazon.S3;
using Amazon.S3.Util;
using CrudAwsS3;

AmazonS3Client s3Client = new();
S3Service s3Service = new(s3Client);

if (await AmazonS3Util.DoesS3BucketExistV2Async(s3Client,"test-bucket-net-core-app"))
{
    s3Service.CreateFile();
    
    Console.WriteLine(await s3Service.GetFileContent());
    
    s3Service.DeleteFile();
}