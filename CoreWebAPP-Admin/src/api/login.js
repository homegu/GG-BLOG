import request from '@/utils/request'

export function login(userName, pwd) {
  return request({
    url: '/api/System/Login',
    method: 'post',
    data: {
      UserName: userName,
      Password: pwd
    }
  })
}

export function getInfo() {
  return request({
    url: '/api/User/info',
    method: 'get',
    params: {}
  })
}

export function logout(data) {
  return request({
    url: '/api/System/Logout',
    method: 'post',
    data:data
  })
}
