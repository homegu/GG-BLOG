import request from '@/utils/request'

export function login(userName, pwd) {
  return request({
    url: '/api/User',
    method: 'post',
    params: {
      userName:userName,
      pwd:pwd
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
    url: '/user/logout',
    method: 'post'
  })
}
