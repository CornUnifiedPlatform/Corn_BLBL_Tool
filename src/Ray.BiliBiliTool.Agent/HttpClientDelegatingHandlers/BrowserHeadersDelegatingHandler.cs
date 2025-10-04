namespace Ray.BiliBiliTool.Agent.HttpClientDelegatingHandlers;

public class BrowserHeadersDelegatingHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken
    )
    {
        // 只为 api.bilibili.com 的请求添加这些头部
        if (request.RequestUri?.Host == "api.bilibili.com")
        {
            // 添加浏览器指纹头部
            request.Headers.TryAddWithoutValidation("sec-ch-ua", 
                "\"Chromium\";v=\"140\", \"Not=A?Brand\";v=\"24\", \"Microsoft Edge\";v=\"140\"");
            
            request.Headers.TryAddWithoutValidation("sec-ch-ua-mobile", "?0");
            
            request.Headers.TryAddWithoutValidation("sec-ch-ua-platform", "\"Windows\"");
            
            if (!request.Headers.Contains("User-Agent"))
            {
                request.Headers.TryAddWithoutValidation("User-Agent", 
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36 Edg/140.0.0.0");
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
