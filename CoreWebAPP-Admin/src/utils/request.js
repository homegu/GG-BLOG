import axios from 'axios'
import { Message, MessageBox } from 'element-ui'
import store from '../store'
import { getToken } from '@/utils/auth'
import Qs from 'qs'

// 创建axios实例
const service = axios.create({
  baseURL: process.env.BASE_API, // api 的 base_url
  timeout: 5000, // 请求超时时间
  // headers: {'Content-Type': 'application/x-www-form-urlencoded'},
  // transformRequest: [function (data) {
  //   return Qs.stringify(data);
  //   // var str = [];  
  //   // for (var p in data) {  
  //   //     str.push(encodeURIComponent(p) + "=" + encodeURIComponent(data[p]));  
  //   // }  
  //   // return str.join("&"); 
  // }],
})

// request拦截器
service.interceptors.request.use(
  config => {
    if (store.getters.token) {
      config.headers['Authorization'] = "Bearer " + getToken() // 让每个请求携带自定义token 请根据实际情况自行修改
    }
    return config
  },
  error => {
    // Do something with request error
    console.log(error) // for debug
    Promise.reject(error)
  }
)

// response 拦截器
service.interceptors.response.use(
  response => {
    const res = response.data;
    if (res.code !== 200) {
      Message({
        message: res.message,
        type: 'error',
        duration: 5 * 1000
      });
      return Promise.reject('error')
    } else if (res.code === 999) {

    } else if (res.code === 401) {

    } else if (res.code === 200) {
      return response.data
    }
  },
  error => {
    console.log('err' + error) // for debug
    Message({
      message: error.message,
      type: 'error',
      duration: 5 * 1000
    })
    return Promise.reject(error)
  }
)

export default service
