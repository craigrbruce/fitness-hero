import request from 'axios';
import { isArray } from 'lodash';

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
  request.post(url);
}

export function save(url, params) {
  return !params.id || params.id === 0 ?
    request.post(url, params) :
    request.put(`${url}/${params.id}`, params);
}

export function get(url, params) {
  if (isArray(params)) {
    return request.get(`${url}${getIdsQueryString(params)}`);
  }
  return request.get(url, { params });
}

export function deleteResource(url: string) {
  return request.delete(url);
}

export function bulkDeleteResource(url: string, ids: Array<number>) {
  if (ids.length === 1) {
    return request.delete(`${url}/${ids[0]}`);
  }

  return request.delete(`${url}${getIdsQueryString(ids)}`);
}
