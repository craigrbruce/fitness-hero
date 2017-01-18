import request from 'axios';
import { isArray } from 'lodash';

function getHeaders() {
  return {
    Authorization: 'Bearer the bear', // `Bearer ${localStorage.getItem('i_dunno_some_token_thing_perhaps_cookie')}`,
    'Content-Type': 'application/json',
  };
}

function getIdsQueryString(ids) {
  if (!ids || ids.length === 0) {
    return '';
  }
  return ids.reduce(
    (previousValue, currentValue, currentIndex) => (
      `${previousValue}${currentValue}${currentIndex + 1 === ids.length ? '' : '&ids='}`
    ),
    '?ids=');
}

/* if you are posting a body use `save` */
export function post(url) {
  const headers = getHeaders();
  request.post(url, { headers });
}

export function save(url, params) {
  const headers = getHeaders();
  return !params.id || params.id === 0 ?
    request.post(url, params, { headers }) :
    request.put(`${url}/${params.id}`, params, { headers });
}

export function get(url, params) {
  const headers = getHeaders();
  if (isArray(params)) {
    return request.get(`${url}${getIdsQueryString(params)}`, { headers });
  }
  return request.get(url, { params, headers });
}

export function deleteResource(url: string) {
  const headers = getHeaders();
  return request.delete(url, { headers });
}

export function bulkDeleteResource(url: string, ids: Array<number>) {
  const headers = getHeaders();
  if (ids.length === 1) {
    return request.delete(`${url}/${ids[0]}`, { headers });
  }

  return request.delete(`${url}${getIdsQueryString(ids)}`, { headers });
}
