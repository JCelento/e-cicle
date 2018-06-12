import superagentPromise from 'superagent-promise';
import _superagent from 'superagent';

const superagent = superagentPromise(_superagent, global.Promise);

const API_ROOT = 'http://localhost:5000';

const encode = encodeURIComponent;
const responseBody = res => res.body;

let token = null;
const tokenPlugin = req => {
  if (token) {
    req.set('authorization', `Bearer ${token}`);
  }
}

const requests = {
  del: url =>
    superagent.del(`${API_ROOT}${url}`).use(tokenPlugin).then(responseBody),
  get: url =>
    superagent.get(`${API_ROOT}${url}`).use(tokenPlugin).then(responseBody),
  put: (url, body) =>
    superagent.put(`${API_ROOT}${url}`, body).use(tokenPlugin).then(responseBody),
  post: (url, body) =>
    superagent.post(`${API_ROOT}${url}`, body).use(tokenPlugin).then(responseBody)
};

const Auth = {
  current: () =>
    requests.get('/user'),
  login: (email, password) =>
    requests.post('/users/login', { user: { email, password } }),
  register: (username, email, password) =>
    requests.post('/users', { user: { username, email, password } }),
  save: user =>
    requests.put('/user', { user })
};

const Tags = {
  getAll: () => requests.get('/tags')
};

const limit = (count, p) => `limit=${count}&offset=${p ? p * count : 0}`;
const omitProjectSlug = project => Object.assign({}, project, { slug: undefined })
const omitComponentSlug = component => Object.assign({}, component, { slug: undefined })

const Projects = {
  all: page =>
    requests.get(`/projects?${limit(10, page)}`),
  bySearch: (search, page) => 
    requests.get(`/projects?search=${encode(search)}&${limit(10, page)}`),  
  byAuthor: (author, page) =>
    requests.get(`/projects?author=${encode(author)}&${limit(5, page)}`),
  byTag: (tag, page) =>
    requests.get(`/projects?tag=${encode(tag)}&${limit(10, page)}`),
  del: slug =>
    requests.del(`/projects/${slug}`),
  favorite: slug =>
    requests.post(`/projects/${slug}/favorite`),
  favoritedBy: (author, page) =>
    requests.get(`/projects?favorited=${encode(author)}&${limit(5, page)}`),
  feed: () =>
    requests.get('/projects/feed?limit=10&offset=0'),
  get: slug =>
    requests.get(`/projects/${slug}`),
  unfavorite: slug =>
    requests.del(`/projects/${slug}/favorite`),
  update: project =>
    requests.put(`/projects/${project.slug}`, { project: omitProjectSlug(project) }),
  create: project =>
    requests.post('/projects', { project })
};

const Components = {
get: slug =>
  requests.get(`/components/${slug}`),
all: page =>
  requests.get(`/components?`),
update: component =>
  requests.put(`/components/${component.slug}`, { component: omitComponentSlug(component) }),
create: component =>
  requests.post('/components', { component })
};

const Comments = {
  create: (slug, comment) =>
    requests.post(`/projects/${slug}/comments`, { comment }),
  delete: (slug, commentId) =>
    requests.del(`/projects/${slug}/comments/${commentId}`),
  forProject: slug =>
    requests.get(`/projects/${slug}/comments`)
};

const Profile = {
  follow: username =>
    requests.post(`/profiles/${username}/follow`),
  get: username =>
    requests.get(`/profiles/${username}`),
  unfollow: username =>
    requests.del(`/profiles/${username}/follow`)
};

export default {
  Projects,
  Auth,
  Comments,
  Profile,
  Tags,
  Components,
  setToken: _token => { token = _token; }
};
