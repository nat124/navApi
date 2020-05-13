using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Internal;


namespace TestCore
{
    public class S3Service
    {
        private static String accessKey = "AKIAJ5ZP4Q6M53E6ENMA";
        private static String accessSecret = "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";
        private static String bucket = "pistis";
        private static String bucket150 = "pistis150x150";
        private static String bucket450 = "pistis450x450";

        //public static async Task<UploadPhotoModel> UploadObject(string file)
        //{

        //    try
        //    {
        //        // connecting to the client
        //        var fileExtension = ".";
        //        var FileData = file.Split(',');
        //        file = FileData[1];
        //        if (FileData[0] != null)
        //            fileExtension += FileData[0].Split('/')[1].Split(';')[0];

        //        var client = new AmazonS3Client(accessKey, accessSecret, Amazon.RegionEndpoint.USEast2);

        //        // get the file and convert it to the byte[]
        //        byte[] fileBytes = Convert.FromBase64String(file);



        //            // create unique file name 
        //            var fileName = Guid.NewGuid().ToString() + fileExtension;

        //            PutObjectResponse response = null;
        //            PutObjectResponse response150 = null;
        //            PutObjectResponse response450 = null;
        //            var stream = new MemoryStream(fileBytes);

        //            Image Oimg = Image.FromStream(stream);
        //            var width = Oimg.Width;
        //            var height = Oimg.Height;
        //            //For Orignal Image
        //            try
        //        {
        //            using (stream)
        //            {
        //                var request = new PutObjectRequest
        //                {
        //                    BucketName = bucket,
        //                    Key = fileName,
        //                    InputStream = stream,
        //                    //ContentType = file.ContentType,
        //                    CannedACL = S3CannedACL.PublicRead,
        //                };
        //                response = await client.PutObjectAsync(request);
        //            }
        //        }
        //        catch(Exception ex)
        //        {
        //            throw ex;
        //        }

        //        if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            return new UploadPhotoModel
        //            {
        //                Success = true,
        //                FileName = fileName
        //            };
        //        }
        //        else
        //        {
        //            return new UploadPhotoModel
        //            {
        //                Success = false,
        //                FileName = fileName,
        //                code= response.HttpStatusCode,
        //            };
        //        }
        //    }
        //    catch (AmazonS3Exception amazonS3Exception)
        //    {
        //        if (
        //        amazonS3Exception.ErrorCode != null &&
        //        (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
        //        amazonS3Exception.ErrorCode.Equals("InvalidSecurity"))
        //        )
        //        {
        //            Console.WriteLine("Please check the provided AWS Credentials.");
        //            Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");

        //        }
        //        else
        //        {
        //            Console.WriteLine("An Error, number {0}, occurred when listing buckets with the message '{1}", amazonS3Exception.ErrorCode, amazonS3Exception.Message);
        //        }
        //        throw amazonS3Exception;
        //    }
        //}

        public static async Task<UploadPhotoModel> UploadObject(string file)
        {
            try
            {
                // connecting to the client
                var fileExtension = ".";
                var FileData = file.Split(',');
                file = FileData[1];
                if (FileData[0] != null)
                    fileExtension += FileData[0].Split('/')[1].Split(';')[0];

                var client = new AmazonS3Client(accessKey, accessSecret, Amazon.RegionEndpoint.USEast2);

                // get the file and convert it to the byte[]
                //byte[] fileBytes = new Byte[file.Length];
                //file.OpenReadStream().Read(fileBytes, 0, Int32.Parse(file.Length.ToString()));

                byte[] fileBytes = Convert.FromBase64String(file);


                // create unique file name 
                var fileName = Guid.NewGuid().ToString() + fileExtension;

                PutObjectResponse response = null;

                using (var stream = new MemoryStream(fileBytes))
                {
                    var request = new PutObjectRequest
                    {
                        BucketName = bucket,
                        Key = fileName,
                        InputStream = stream,
                        //ContentType = file.ContentType,
                        CannedACL = S3CannedACL.PublicRead,
                    };

                    response = await client.PutObjectAsync(request);
                };

                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return new UploadPhotoModel
                    {
                        Success = true,
                        FileName = fileName
                    };
                }
                else
                {
                    return new UploadPhotoModel
                    {
                        Success = false,
                        FileName = fileName
                    };
                }
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (
                amazonS3Exception.ErrorCode != null &&
                (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                amazonS3Exception.ErrorCode.Equals("InvalidSecurity"))
                )
                {
                    Console.WriteLine("Please check the provided AWS Credentials.");
                    Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");

                }
                else
                {
                    Console.WriteLine("An Error, number {0}, occurred when listing buckets with the message '{1}", amazonS3Exception.ErrorCode, amazonS3Exception.Message);
                }
                throw amazonS3Exception;
            }
        }
        public static async Task<UploadPhotoModel> UploadObject150(string file,string fileName)
        {
            try
            {
                // connecting to the client
                var fileExtension = ".";
                var FileData = file.Split(',');
                file = FileData[1];
                if (FileData[0] != null)
                    fileExtension += FileData[0].Split('/')[1].Split(';')[0];

                var client = new AmazonS3Client(accessKey, accessSecret, Amazon.RegionEndpoint.USEast2);

                byte[] fileBytes = Convert.FromBase64String(file);

                PutObjectResponse response = null;
                byte[] fileBytes150;
                //var streamorignal = new MemoryStream(fileBytes);
                MemoryStream streamorignal = new MemoryStream(fileBytes);
                

                Image img = Image.FromStream(streamorignal);

                var width = img.Width;
                var height = img.Height;
                var givenWidth = 150;
                var givenHeight = 150;
                // Figure out the ratio
                double ratioX = (double)givenWidth / (double)width;
                double ratioY = (double)givenHeight / (double)height;
                // use whichever multiplier is smaller
                double ratio = ratioX < ratioY ? ratioX : ratioY;

                // now we can get the new height and width
                int h = Convert.ToInt32(height * ratio);
                int w = Convert.ToInt32(width * ratio);
                using (Bitmap b = new Bitmap(img, new Size(w, h)))
                {
                    using (MemoryStream ms2 = new MemoryStream())
                    {
                        b.Save(ms2, System.Drawing.Imaging.ImageFormat.Jpeg);
                        fileBytes150 = ms2.ToArray();
                    }
                }


                using (var stream = new MemoryStream(fileBytes150))
                {
                    var request = new PutObjectRequest
                    {
                        BucketName = bucket150,
                        Key = fileName,
                        InputStream = stream,
                        //ContentType = file.ContentType,
                        CannedACL = S3CannedACL.PublicRead,
                    };

                    response = await client.PutObjectAsync(request);
                };

                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return new UploadPhotoModel
                    {
                        Success = true,
                        FileName = fileName
                    };
                }
                else
                {
                    return new UploadPhotoModel
                    {
                        Success = false,
                        FileName = fileName
                    };
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            //catch (AmazonS3Exception amazonS3Exception)
            //{
            //    if (
            //    amazonS3Exception.ErrorCode != null &&
            //    (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
            //    amazonS3Exception.ErrorCode.Equals("InvalidSecurity"))
            //    )
            //    {
            //        Console.WriteLine("Please check the provided AWS Credentials.");
            //        Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");

            //    }
            //    else
            //    {
            //        Console.WriteLine("An Error, number {0}, occurred when listing buckets with the message '{1}", amazonS3Exception.ErrorCode, amazonS3Exception.Message);
            //    }
            //    throw amazonS3Exception;
            //}
        }
        public static async Task<UploadPhotoModel> UploadObject450(string file, string fileName)
        {
            try
            {
                // connecting to the client
                var fileExtension = ".";
                var FileData = file.Split(',');
                file = FileData[1];
                if (FileData[0] != null)
                    fileExtension += FileData[0].Split('/')[1].Split(';')[0];

                var client = new AmazonS3Client(accessKey, accessSecret, Amazon.RegionEndpoint.USEast2);

                // get the file and convert it to the byte[]
                //byte[] fileBytes = new Byte[file.Length];
                //file.OpenReadStream().Read(fileBytes, 0, Int32.Parse(file.Length.ToString()));

                byte[] fileBytes = Convert.FromBase64String(file);


                // create unique file name 
               // var fileName = Guid.NewGuid().ToString() + fileExtension;

                PutObjectResponse response = null;
                var stream = new MemoryStream(fileBytes);

                Image Oimg = Image.FromStream(stream);
                var width = Oimg.Width;
                var height = Oimg.Height;
                var givenWidth = 450;
                var givenHeight = 450;
                    try
                    {
                        byte[] fileBytes450;
                        var streamorignal = new MemoryStream(fileBytes);
                        Image img = Image.FromStream(streamorignal);
                    // Figure out the ratio
                    double ratioX = (double)givenWidth / (double)width;
                    double ratioY = (double)givenHeight / (double)height;
                    // use whichever multiplier is smaller
                    double ratio = ratioX < ratioY ? ratioX : ratioY;

                    // now we can get the new height and width
                    int h = Convert.ToInt32(height * ratio);
                    int w = Convert.ToInt32(width * ratio);

                    using (Bitmap b = new Bitmap(img, new Size(w, h)))
                        {
                            using (MemoryStream ms2 = new MemoryStream())
                            {
                                b.Save(ms2, System.Drawing.Imaging.ImageFormat.Jpeg);
                                fileBytes450 = ms2.ToArray();
                            }
                        }

                        using (var stream450 = new MemoryStream(fileBytes450))
                        {
                            var request = new PutObjectRequest
                            {
                                BucketName = bucket450,
                                Key = fileName,
                                InputStream = stream450,
                                //ContentType = file.ContentType,
                                CannedACL = S3CannedACL.PublicRead,
                            };

                            response = await client.PutObjectAsync(request);
                        };
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return new UploadPhotoModel
                    {
                        Success = true,
                        FileName = fileName
                    };
                }
                else
                {
                    return new UploadPhotoModel
                    {
                        Success = false,
                        FileName = fileName
                    };
                }
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (
                amazonS3Exception.ErrorCode != null &&
                (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                amazonS3Exception.ErrorCode.Equals("InvalidSecurity"))
                )
                {
                    Console.WriteLine("Please check the provided AWS Credentials.");
                    Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");

                }
                else
                {
                    Console.WriteLine("An Error, number {0}, occurred when listing buckets with the message '{1}", amazonS3Exception.ErrorCode, amazonS3Exception.Message);
                }
                throw amazonS3Exception;
            }
        }

        //public static async Task<UploadPhotoModel> UploadObject150(string file,string fileName)
        //{

        //    try
        //    {
        //        // connecting to the client
        //        var fileExtension = ".";
        //        var FileData = file.Split(',');
        //        file = FileData[1];
        //        if (FileData[0] != null)
        //            fileExtension += FileData[0].Split('/')[1].Split(';')[0];

        //        var client = new AmazonS3Client(accessKey, accessSecret, Amazon.RegionEndpoint.USEast2);

        //        // get the file and convert it to the byte[]
        //        byte[] fileBytes = Convert.FromBase64String(file);



        //        // create unique file name 
        //        //var fileName = Guid.NewGuid().ToString() + fileExtension;

        //        PutObjectResponse response = null;
        //        PutObjectResponse response150 = null;
        //        PutObjectResponse response450 = null;
        //        var stream = new MemoryStream(fileBytes);

        //        Image Oimg = Image.FromStream(stream);
        //        var width = Oimg.Width;
        //        var height = Oimg.Height;

        //        //150*150
        //        try
        //        {
        //            byte[] fileBytes150;
        //            var streamorignal = new MemoryStream(fileBytes);
        //            Image img = Image.FromStream(streamorignal);
        //            using (Bitmap b = new Bitmap(img, new Size(150, 150)))
        //            {
        //                using (MemoryStream ms2 = new MemoryStream())
        //                {
        //                    b.Save(ms2, System.Drawing.Imaging.ImageFormat.Jpeg);
        //                    fileBytes150 = ms2.ToArray();
        //                }
        //            }



        //            var stream150 = new MemoryStream(fileBytes150);
        //            using (stream150)
        //            {
        //                var request150 = new PutObjectRequest
        //                {
        //                    BucketName = bucket150,
        //                    Key = fileName,
        //                    InputStream = stream150,
        //                    //ContentType = file.ContentType,
        //                    CannedACL = S3CannedACL.PublicRead,
        //                };

        //                response150 = await client.PutObjectAsync(request150);
        //            };
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }


        //        if (response150.HttpStatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            return new UploadPhotoModel
        //            {
        //                Success = true,
        //                FileName = fileName
        //            };
        //        }
        //        else
        //        {
        //            return new UploadPhotoModel
        //            {
        //                Success = false,
        //                FileName = fileName
        //            };
        //        }
        //    }
        //    catch (AmazonS3Exception amazonS3Exception)
        //    {
        //        if (
        //        amazonS3Exception.ErrorCode != null &&
        //        (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
        //        amazonS3Exception.ErrorCode.Equals("InvalidSecurity"))
        //        )
        //        {
        //            Console.WriteLine("Please check the provided AWS Credentials.");
        //            Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");

        //        }
        //        else
        //        {
        //            Console.WriteLine("An Error, number {0}, occurred when listing buckets with the message '{1}", amazonS3Exception.ErrorCode, amazonS3Exception.Message);
        //        }
        //        throw amazonS3Exception;
        //    }
        //}
        //public static async Task<UploadPhotoModel> UploadObject450(string file,string fileName)
        //{

        //    try
        //    {
        //        // connecting to the client
        //        var fileExtension = ".";
        //        var FileData = file.Split(',');
        //        file = FileData[1];
        //        if (FileData[0] != null)
        //            fileExtension += FileData[0].Split('/')[1].Split(';')[0];

        //        var client = new AmazonS3Client(accessKey, accessSecret, Amazon.RegionEndpoint.USEast2);

        //        // get the file and convert it to the byte[]
        //        byte[] fileBytes = Convert.FromBase64String(file);



        //        // create unique file name 
        //       // var fileName = Guid.NewGuid().ToString() + fileExtension;

        //        PutObjectResponse response = null;
        //        PutObjectResponse response150 = null;
        //        PutObjectResponse response450 = null;
        //        var stream = new MemoryStream(fileBytes);

        //        Image Oimg = Image.FromStream(stream);
        //        var width = Oimg.Width;
        //        var height = Oimg.Height;

        //        //450*450
        //        if (width > 450 || height > 450)
        //        {
        //            try
        //            {
        //                byte[] fileBytes450;
        //                var streamorignal = new MemoryStream(fileBytes);
        //                Image img = Image.FromStream(streamorignal);
        //                var h = 450;
        //                var w = 450;
        //                if (h > height)
        //                    h = height;
        //                if (w < width)
        //                    w = width;
        //                using (Bitmap b = new Bitmap(img, new Size(w, h)))
        //                {
        //                    using (MemoryStream ms2 = new MemoryStream())
        //                    {
        //                        b.Save(ms2, System.Drawing.Imaging.ImageFormat.Jpeg);
        //                        fileBytes450 = ms2.ToArray();
        //                    }
        //                }
        //                var stream450 = new MemoryStream(fileBytes450);
        //                using (stream450)
        //                {
        //                    var request450 = new PutObjectRequest
        //                    {
        //                        BucketName = bucket450,
        //                        Key = fileName,
        //                        InputStream = stream450,
        //                        //ContentType = file.ContentType,
        //                        CannedACL = S3CannedACL.PublicRead,
        //                    };

        //                    response450 = await client.PutObjectAsync(request450);
        //                };
        //            }
        //            catch (Exception ex)
        //            {
        //                throw ex;
        //            }

        //        }
        //        else
        //        {
        //            var stream1 = new MemoryStream(fileBytes);

        //            //For Orignal Image
        //            using (stream1)
        //            {
        //                var request = new PutObjectRequest
        //                {
        //                    BucketName = bucket450,
        //                    Key = fileName,
        //                    InputStream = stream1,
        //                    //ContentType = file.ContentType,
        //                    CannedACL = S3CannedACL.PublicRead,
        //                };
        //                response450 = await client.PutObjectAsync(request);
        //            }
        //        }
        //        if (response450.HttpStatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            return new UploadPhotoModel
        //            {
        //                Success = true,
        //                FileName = fileName
        //            };
        //        }
        //        else
        //        {
        //            return new UploadPhotoModel
        //            {
        //                Success = false,
        //                FileName = fileName
        //            };
        //        }
        //    }
        //    catch (AmazonS3Exception amazonS3Exception)
        //    {
        //        if (
        //        amazonS3Exception.ErrorCode != null &&
        //        (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
        //        amazonS3Exception.ErrorCode.Equals("InvalidSecurity"))
        //        )
        //        {
        //            Console.WriteLine("Please check the provided AWS Credentials.");
        //            Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");

        //        }
        //        else
        //        {
        //            Console.WriteLine("An Error, number {0}, occurred when listing buckets with the message '{1}", amazonS3Exception.ErrorCode, amazonS3Exception.Message);
        //        }
        //        throw amazonS3Exception;
        //    }
        //}
        public static async Task<UploadPhotoModel> RemoveObject(String fileName)
        {
            var client = new AmazonS3Client(accessKey, accessSecret, Amazon.RegionEndpoint.USEast2);
            //for orignal
            var request = new DeleteObjectRequest
            {
                BucketName = bucket,
                Key = fileName
            };

            var response = await client.DeleteObjectAsync(request);


            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return new UploadPhotoModel
                {
                    Success = true,
                    FileName = fileName
                };
            }
            else
            {
                return new UploadPhotoModel
                {
                    Success = false,
                    FileName = fileName
                };
            }
        }
        public static async Task<byte[]> GetReaderFromS3(string fileName)
        {
            fileName = fileName.Split('/')[3];
            var _S3Client = new AmazonS3Client(accessKey, accessSecret, Amazon.RegionEndpoint.USEast2);
            byte[] resultByte = null;
            using (MemoryStream ms = new MemoryStream())
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = bucket,
                    Key = fileName,
                };
                var response = await _S3Client.GetObjectAsync(request).ConfigureAwait(false);
                await response.ResponseStream.CopyToAsync(ms).ConfigureAwait(false);
                resultByte = ms.ToArray();
            }
            return resultByte;
        }


        public static async Task<UploadPhotoModel> updateUploadObject150(string file, string fileName)
        {
            try
            {
                // connecting to the client
                

                var client = new AmazonS3Client(accessKey, accessSecret, Amazon.RegionEndpoint.USEast2);

                // get the file and convert it to the byte[]
                //byte[] fileBytes = new Byte[file.Length];
                //file.OpenReadStream().Read(fileBytes, 0, Int32.Parse(file.Length.ToString()));

                byte[] fileBytes = Convert.FromBase64String(file);


                // create unique file name 
                //var fileName = Guid.NewGuid().ToString() + fileExtension;

                PutObjectResponse response = null;
                byte[] fileBytes150;
                var streamorignal = new MemoryStream(fileBytes);
                Image img = Image.FromStream(streamorignal);

                var width = img.Width;
                var height = img.Height;
                var givenWidth = 150;
                var givenHeight = 150;
                // Figure out the ratio
                double ratioX = (double)givenWidth / (double)width;
                double ratioY = (double)givenHeight / (double)height;
                // use whichever multiplier is smaller
                double ratio = ratioX < ratioY ? ratioX : ratioY;

                // now we can get the new height and width
                int h = Convert.ToInt32(height * ratio);
                int w = Convert.ToInt32(width * ratio);
                using (Bitmap b = new Bitmap(img, new Size(w, h)))
                {
                    using (MemoryStream ms2 = new MemoryStream())
                    {
                        b.Save(ms2, System.Drawing.Imaging.ImageFormat.Jpeg);
                        fileBytes150 = ms2.ToArray();
                    }
                }


                using (var stream = new MemoryStream(fileBytes150))
                {
                    var request = new PutObjectRequest
                    {
                        BucketName = bucket150,
                        Key = fileName,
                        InputStream = stream,
                        //ContentType = file.ContentType,
                        CannedACL = S3CannedACL.PublicRead,
                    };

                    response = await client.PutObjectAsync(request);
                };

                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return new UploadPhotoModel
                    {
                        Success = true,
                        FileName = fileName
                    };
                }
                else
                {
                    return new UploadPhotoModel
                    {
                        Success = false,
                        FileName = fileName
                    };
                }
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (
                amazonS3Exception.ErrorCode != null &&
                (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                amazonS3Exception.ErrorCode.Equals("InvalidSecurity"))
                )
                {
                    Console.WriteLine("Please check the provided AWS Credentials.");
                    Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");

                }
                else
                {
                    Console.WriteLine("An Error, number {0}, occurred when listing buckets with the message '{1}", amazonS3Exception.ErrorCode, amazonS3Exception.Message);
                }
                throw amazonS3Exception;
            }
        }
        public static async Task<UploadPhotoModel> updateUploadObject450(string file, string fileName)
        {
            try
            {
                

                var client = new AmazonS3Client(accessKey, accessSecret, Amazon.RegionEndpoint.USEast2);

                // get the file and convert it to the byte[]
                //byte[] fileBytes = new Byte[file.Length];
                //file.OpenReadStream().Read(fileBytes, 0, Int32.Parse(file.Length.ToString()));

                byte[] fileBytes = Convert.FromBase64String(file);


                // create unique file name 
                // var fileName = Guid.NewGuid().ToString() + fileExtension;

                PutObjectResponse response = null;
                var stream = new MemoryStream(fileBytes);

                Image Oimg = Image.FromStream(stream);
                var width = Oimg.Width;
                var height = Oimg.Height;
                var givenWidth = 450;
                var givenHeight = 450;
                try
                {
                    byte[] fileBytes450;
                    var streamorignal = new MemoryStream(fileBytes);
                    Image img = Image.FromStream(streamorignal);
                    // Figure out the ratio
                    double ratioX = (double)givenWidth / (double)width;
                    double ratioY = (double)givenHeight / (double)height;
                    // use whichever multiplier is smaller
                    double ratio = ratioX < ratioY ? ratioX : ratioY;

                    // now we can get the new height and width
                    int h = Convert.ToInt32(height * ratio);
                    int w = Convert.ToInt32(width * ratio);

                    using (Bitmap b = new Bitmap(img, new Size(w, h)))
                    {
                        using (MemoryStream ms2 = new MemoryStream())
                        {
                            b.Save(ms2, System.Drawing.Imaging.ImageFormat.Jpeg);
                            fileBytes450 = ms2.ToArray();
                        }
                    }

                    using (var stream450 = new MemoryStream(fileBytes450))
                    {
                        var request = new PutObjectRequest
                        {
                            BucketName = bucket450,
                            Key = fileName,
                            InputStream = stream450,
                            //ContentType = file.ContentType,
                            CannedACL = S3CannedACL.PublicRead,
                        };

                        response = await client.PutObjectAsync(request);
                    };
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return new UploadPhotoModel
                    {
                        Success = true,
                        FileName = fileName
                    };
                }
                else
                {
                    return new UploadPhotoModel
                    {
                        Success = false,
                        FileName = fileName
                    };
                }
            }
            catch (AmazonS3Exception amazonS3Exception)
            {
                if (
                amazonS3Exception.ErrorCode != null &&
                (amazonS3Exception.ErrorCode.Equals("InvalidAccessKeyId") ||
                amazonS3Exception.ErrorCode.Equals("InvalidSecurity"))
                )
                {
                    Console.WriteLine("Please check the provided AWS Credentials.");
                    Console.WriteLine("If you haven't signed up for Amazon S3, please visit http://aws.amazon.com/s3");

                }
                else
                {
                    Console.WriteLine("An Error, number {0}, occurred when listing buckets with the message '{1}", amazonS3Exception.ErrorCode, amazonS3Exception.Message);
                }
                throw amazonS3Exception;
            }
        }

    }
}