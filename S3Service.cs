using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace CrudAwsS3;

internal class S3Service
{
    private readonly AmazonS3Client _s3Client;
    private const string BucketName = "test-bucket-net-core-app";
    private const string FileName = "TestFileForS3.txt";

    public S3Service(AmazonS3Client s3Client)
    {
        _s3Client = s3Client;
    }

    internal async void CreateFile()
    {
        TransferUtility transferUtility = new(_s3Client);
        await transferUtility.UploadAsync(
            $"C:\\path\\to\\file\\{FileName}",
            BucketName);
    }

    internal async Task<string> GetFileContent()
    {
        var request = new GetObjectRequest
        {
            BucketName = BucketName,
            Key = FileName
        };
        var response = await _s3Client.GetObjectAsync(request);
        using var reader = new StreamReader(response.ResponseStream);
        return await reader.ReadToEndAsync();
    }

    internal async void DeleteFile()
    {
        await _s3Client.DeleteObjectAsync(BucketName, FileName);
    }
}