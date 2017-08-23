var config = 
    {
        resultTemplate : `
                            <div class="col-xs-12 col-sm-6 col-md-4 result-view" data-url="{{url}}" > 
                                <div class="result-template">
                                    <h4 title="{{url}}">{{urlTitle}}</h4>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <img class="img-responsive" src="{{imageUrl}}" alt="sample image" />
                                        </div>
                                        <div class="col-xs-12">
                                            <h5>Document complete</h5>
                                        </div>
                                        <div class="col-xs-offset-4 col-xs-4 ch">
                                            <span>1st View</span>
                                        </div>
                                        <div class="col-xs-4 ch">
                                            <span>Repeat</span>
                                        </div>
                                        <div class="col-xs-4 th">
                                            <span>Load Time</span>
                                        </div>
                                        <div class="col-xs-4 td1">
                                            <span>{{loadTime1}}ms</span>
                                        </div>
                                        <div class="col-xs-4 td2">
                                            <span>{{loadTime2}}ms</span>
                                        </div>
                                        <div class="col-xs-4 th">
                                            <span>First Byte</span>
                                        </div>
                                        <div class="col-xs-4 td1">
                                            <span>{{firstByte1}}</span>
                                        </div>
                                        <div class="col-xs-4 td2">
                                            <span>{{firstByte2}}</span>
                                        </div>
                                        <div class="col-xs-4 th">
                                            <span>Start Render</span>
                                        </div>
                                        <div class="col-xs-4 td1">
                                            <span>{{startRender1}}ms</span>
                                        </div>
                                        <div class="col-xs-4 td2">
                                            <span>{{startRender2}}ms</span>
                                        </div>
                                        <div class="col-xs-4 th">
                                            <span>Reqeusts</span>
                                        </div>
                                        <div class="col-xs-4 td1">
                                            <span>{{requests1}}</span>
                                        </div>
                                        <div class="col-xs-4 td2">
                                            <span>{{requests2}}</span>
                                        </div>            
                                        <div class="col-xs-4 th">
                                            <span>Bytes In</span>
                                        </div>
                                        <div class="col-xs-4 td1">
                                            <span>{{bytesIn1}}</span>
                                        </div>
                                        <div class="col-xs-4 td2">
                                            <span>{{bytesIn2}}</span>
                                        </div>
                                        <div class="col-xs-4 th">
                                            <span>Speed Index</span>
                                        </div>
                                        <div class="col-xs-4 td1">
                                            <span>{{speedIndex1}}</span>
                                        </div>
                                        <div class="col-xs-4 td2">
                                            <span>{{speedIndex2}}</span>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <h5>Fully Loaded</h5>
                                        </div>
                                        <div class="col-xs-4 th">
                                            <span>Load Time</span>
                                        </div>
                                        <div class="col-xs-4 td1">
                                            <span>{{fLoadTime1}}ms</span>
                                        </div>
                                        <div class="col-xs-4 td2">
                                            <span>{{fLoadTime2}}ms</span>
                                        </div>
                                        <div class="col-xs-4 th">
                                            <span>Reqeusts</span>
                                        </div>
                                        <div class="col-xs-4 td1">
                                            <span>{{fRequests1}}</span>
                                        </div>
                                        <div class="col-xs-4 td2">
                                            <span>{{fRequests2}}</span>
                                        </div>    
                                        <div class="col-xs-4 th">
                                            <span>Bytes In</span>
                                        </div>
                                        <div class="col-xs-4 td1">
                                            <span>{{fBytesIn1}}</span>
                                        </div>
                                        <div class="col-xs-4 td2">
                                            <span>{{fBytesIn2}}</span>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        `,
        resultPendingTemplate: `
                              <div class="col-xs-12 col-sm-6 col-md-4 result-view" data-url="{{url}}"> 
                                <div class="result-template">
                                    <h4 title="{{url}}">{{urlTitle}}</h4>
                                    <div class="row">
                                        <div class="col-xs-12">
                                            <img id="wait" class="img-responsive" src="/Content/img/wait.gif" alt="sample image" />
											<p class="status-pending">{{status}}</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        `,
        detailTemplateWrapper: `
                                <div class="col-xs-12 col-sm-6 col-md-8 result-view" data-url="{{url}}" > 
                                    <div class="result-template">
                                        <h4>Requests Data</h4>
                                        <div class="row">
                                            <div class="col-xs-offset-1 col-xs-2 ch">
                                                <span>Mime Type</span>
                                            </div>
                                            <div class="col-xs-2 ch">
                                                <span>Requests</span>
                                            </div>
                                            <div class="col-xs-2 ch">
                                                <span>Requests (Repeat)</span>
                                            </div>
                                            <div class="col-xs-2 ch">
                                                <span>Bytes</span>
                                            </div>
                                            <div class="col-xs-2 ch">
                                                <span>Bytes (Repeat)</span>
                                            </div>
                                        </div>
                                        {{detailTemplateRequests}}
                                     </div>
                                 </div>
                                 {{detailTemplateImages}}
                                `,
        detailTemplateRequests: `
                                  <div class="row">
                                    <div class="col-xs-offset-1 col-xs-2 th">
                                        <span class='mime-type'>{{mimeType}}</span>
                                    </div>
                                    <div class="col-xs-2 td1">
                                        <span>{{requests}}</span>
                                    </div>
                                    <div class="col-xs-2 td1">
                                        <span>{{requests2}}</span>
                                    </div>
                                    <div class="col-xs-2 td1">
                                        <span>{{bytes}}</span>
                                    </div>
                                    <div class="col-xs-2 td2">
                                        <span>{{bytes2}}</span>
                                    </div>
                                  </div>
                                `,
        detailTemplateImages: `
                                <div class="col-xs-12 col-sm-6 col-md-8" >
                                    <h4>Screen shot</h4>
                                    <img class="img-responsive" src="{{screenShot}}" alt="sample image" />
                                </div>

                                <div class="col-xs-12" >
                                    <h4>Check List</h4>
                                    <img class="img-responsive" src="{{checklist}}" alt="sample image" />
                                </div>
                                <br/>
                                <div class="col-xs-12">
                                <h4>Waterfall</h4>
                                    <img class="img-responsive" src="{{waterfall}}" alt="sample image" />
                                </div>
                                <br/>
                                <div class="col-xs-12">
                                <h4>Connection View</h4>
                                    <img class="img-responsive" src="{{connectionView}}" alt="sample image" />
                                </div>
                             `
    }