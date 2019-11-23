using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;

namespace TracerDll
{
    public class Tracer : ITracer
    {
        private readonly List<Stopwatch> stopwatchList = new List<Stopwatch>();
        private readonly List<Dictionary<int, TraceResult>> buffer = new List<Dictionary<int, TraceResult>>();
        private readonly List<TraceResult> results = new List<TraceResult>();
         

        public void StartTrace()
        {
            stopwatchList.Add(new Stopwatch());
            stopwatchList[stopwatchList.Count - 1].Start();
        }
        public void StopTrace()
        {
            TraceResult traceResult = new TraceResult();
            stopwatchList[stopwatchList.Count - 1].Stop();
            traceResult.ms = stopwatchList[stopwatchList.Count - 1].ElapsedMilliseconds;
            stopwatchList.RemoveAt(stopwatchList.Count - 1);

            StackTrace stackTrace = new StackTrace();
            StackFrame[] stackFrames = stackTrace.GetFrames();
            StackFrame callingFrame = stackFrames[1];
            MethodBase method = callingFrame.GetMethod();


            traceResult.methodName = method.Name;
            traceResult.className = method.DeclaringType.Name;
            traceResult.threadId = Thread.CurrentThread.ManagedThreadId;

            traceResult.nestedMethods = new List<TraceResult>();

            traceResult.nesting = stackFrames.Length - 1;

            try
            {
                for (var i = 0; i < buffer.Count; i++)
                {
                    if (buffer[i].TryGetValue(traceResult.threadId, out TraceResult temp))
                    {
                        if (temp.nesting == traceResult.nesting + 1 && temp.threadId == traceResult.threadId)
                        {
                            traceResult.nestedMethods.Add(temp);
                            buffer.Remove(buffer[i]);
                            i--;
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //if (buffer.Count > 0)
            //{
            //    var dic = new Dictionary<int, TraceResult>();
            //    dic.Add(traceResult.threadId, traceResult);
            //    for (int i = 0; i < buffer.Count; i++)
            //    {
            //        if (buffer[i].ElementAt(0).Key != traceResult.threadId && buffer[i].ElementAt(0).Value.methodName != traceResult.methodName)
            //        {
            //            buffer.Add(dic);
            //            break;
            //        }
            //    }

            //}
            //else
            //{
                var dic = new Dictionary<int, TraceResult>();
                dic.Add(traceResult.threadId, traceResult);
                buffer.Add(dic);
            //}

            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].threadId == traceResult.threadId)
                {
                    results.Remove(results[i]);
                    i--;
                }
            }
            results.Add(traceResult);

        }
        public List<TraceResult> GetTraceResult()
        {
            return results;
        }
    }
}




/*
                       * 
 *                 *  *  *  *
 *                     *
 * 
 * */