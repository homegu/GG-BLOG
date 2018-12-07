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

export function getInfo(token) {
  return request({
    url: '/user/info',
    method: 'get',
    params: { token }
  })
}

export function logout() {
  return request({
    url: '/api/System/Logout',
    method: 'post'
  })
}
