// ═══════════════════════════════════════════════
// ProductivityApp — Web Version
// localStorage tabanlı, saf HTML/CSS/JS
// ═══════════════════════════════════════════════

// ── KULLANICI VERİTABANI (demo) ──
const USERS = [{ username: 'admin', password: '1234', fullName: 'Admin Kullanıcı' }];

// ── STATE ──
const state = {
  currentUser: null,
  tasks: [],
  habits: [],
  sessions: [],
  nextTaskId: 1,
  nextHabitId: 1,
  taskFilter: 'all',
  taskSearchQuery: '',
  taskPriorityFilter: 'all',
  editingTaskId: null,
};

// Stack yapısı — silinen görevleri saklar (geri alma için)
const deletedTasksStack = [];

// ── LOCALSTORAGE ──
function save() {
  localStorage.setItem('pa_tasks',    JSON.stringify(state.tasks));
  localStorage.setItem('pa_habits',   JSON.stringify(state.habits));
  localStorage.setItem('pa_sessions', JSON.stringify(state.sessions));
  localStorage.setItem('pa_nextTask', state.nextTaskId);
  localStorage.setItem('pa_nextHabit',state.nextHabitId);
}
function load() {
  state.tasks      = JSON.parse(localStorage.getItem('pa_tasks')    || '[]');
  state.habits     = JSON.parse(localStorage.getItem('pa_habits')   || '[]');
  state.sessions   = JSON.parse(localStorage.getItem('pa_sessions') || '[]');
  state.nextTaskId = parseInt(localStorage.getItem('pa_nextTask')   || '1');
  state.nextHabitId= parseInt(localStorage.getItem('pa_nextHabit') || '1');

  // Örnek veriler (ilk açılış)
  if (state.tasks.length === 0) {
    state.tasks.push({ id: state.nextTaskId++, title: 'Rapor yaz', priority: 'Yüksek', status: 'Bekliyor', due: today(), createdAt: now() });
    state.tasks.push({ id: state.nextTaskId++, title: 'E-postalarını yanıtla', priority: 'Normal', status: 'Devam Ediyor', due: today(), createdAt: now() });
  }
  if (state.habits.length === 0) {
    state.habits.push({ id: state.nextHabitId++, name: 'Sabah egzersizi', streak: 5, lastChecked: today(), createdAt: now() });
    state.habits.push({ id: state.nextHabitId++, name: 'Kitap okuma', streak: 12, lastChecked: today(), createdAt: now() });
  }
  save();
}

function today() { return new Date().toISOString().split('T')[0]; }
function now()   { return new Date().toISOString(); }
function fmtDate(d) { if (!d) return '—'; const dt = new Date(d); return dt.toLocaleDateString('tr-TR'); }

// ── TOAST ──
let toastTimer;
function showToast(msg, color = 'var(--green)') {
  const t = document.getElementById('toast');
  t.textContent = msg;
  t.style.borderLeftColor = color;
  t.style.borderLeft = `4px solid ${color}`;
  t.classList.remove('hidden');
  clearTimeout(toastTimer);
  toastTimer = setTimeout(() => t.classList.add('hidden'), 3000);
}

// ── MODAL ──
let confirmCb = null;
function showConfirm(title, msg, cb) {
  document.getElementById('modal-title').textContent = title;
  document.getElementById('modal-msg').textContent = msg;
  document.getElementById('modal-overlay').classList.remove('hidden');
  confirmCb = cb;
}
document.getElementById('modal-cancel').addEventListener('click', () => {
  document.getElementById('modal-overlay').classList.add('hidden');
  confirmCb = null;
});
document.getElementById('modal-confirm').addEventListener('click', () => {
  if (confirmCb) { confirmCb(); confirmCb = null; }
  document.getElementById('modal-overlay').classList.add('hidden');
});

// ── PAGE NAVIGATION ──
function switchPage(name) {
  document.querySelectorAll('.content-section').forEach(s => s.classList.add('hidden'));
  document.querySelectorAll('.nav-item').forEach(n => n.classList.remove('active'));
  document.getElementById('section-' + name).classList.remove('hidden');
  const ni = document.getElementById('nav-' + name);
  if (ni) ni.classList.add('active');
  // Mobil: sidebar'ı kapat
  document.getElementById('sidebar').classList.remove('open');
  if (name === 'dashboard') renderDashboard();
  if (name === 'tasks')     renderTasks();
  if (name === 'habits')    renderHabits();
  if (name === 'focus')     {} // focus kendi state'ini korur
}
document.querySelectorAll('.nav-item').forEach(item => {
  item.addEventListener('click', e => { e.preventDefault(); switchPage(item.dataset.page); });
});

// ── LOGIN ──
const loginForm = document.getElementById('login-form');
loginForm.addEventListener('submit', e => {
  e.preventDefault();
  const username = document.getElementById('inp-username').value.trim();
  const password = document.getElementById('inp-password').value;
  const user = USERS.find(u => u.username === username && u.password === password);
  const errEl = document.getElementById('login-error');
  if (!user) {
    errEl.classList.remove('hidden');
    document.getElementById('inp-password').value = '';
    return;
  }
  errEl.classList.add('hidden');
  state.currentUser = user;
  document.getElementById('page-login').classList.remove('active');
  document.getElementById('page-login').classList.add('hidden');
  document.getElementById('page-main').classList.remove('hidden');
  document.getElementById('page-main').classList.add('active');
  document.getElementById('sidebar-username').textContent = user.fullName;
  document.getElementById('user-avatar-text').textContent = user.fullName.charAt(0).toUpperCase();
  document.getElementById('welcome-text').textContent = `Hoş geldin, ${user.fullName}! 👋`;
  setDateDisplay();
  load();
  renderDashboard();
  renderTasks();
  renderHabits();
  renderSessions();
});
// Şifre göster/gizle
document.getElementById('btn-toggle-pw').addEventListener('click', () => {
  const inp = document.getElementById('inp-password');
  inp.type = inp.type === 'password' ? 'text' : 'password';
});
// Demo doldur
document.getElementById('inp-username').value = 'admin';
document.getElementById('inp-password').value = '1234';

// ── LOGOUT ──
function logout() {
  state.currentUser = null;
  document.getElementById('page-main').classList.remove('active');
  document.getElementById('page-main').classList.add('hidden');
  document.getElementById('page-login').classList.add('active');
  document.getElementById('page-login').classList.remove('hidden');
  if (focusInterval) { clearInterval(focusInterval); focusInterval = null; }
}
document.getElementById('btn-logout').addEventListener('click', logout);
document.getElementById('btn-logout-mobile').addEventListener('click', logout);

// ── DATE ──
function setDateDisplay() {
  const opts = { weekday:'long', year:'numeric', month:'long', day:'numeric' };
  document.getElementById('date-display').textContent = new Date().toLocaleDateString('tr-TR', opts);
}

// ── MOBILE SIDEBAR ──
document.getElementById('btn-hamburger').addEventListener('click', () => {
  document.getElementById('sidebar').classList.toggle('open');
});

// ══════════════════════════════════════════
// DASHBOARD
// ══════════════════════════════════════════
function renderDashboard() {
  const active = state.tasks.filter(t => t.status !== 'Tamamlandı').length;
  document.getElementById('stat-tasks').textContent   = active;
  document.getElementById('stat-habits').textContent  = state.habits.length;
  document.getElementById('stat-sessions').textContent= state.sessions.length;
  const maxStreak = state.habits.reduce((m, h) => Math.max(m, h.streak), 0);
  document.getElementById('stat-streak').textContent  = maxStreak;
  document.getElementById('badge-tasks').textContent  = active;
  document.getElementById('badge-habits').textContent = state.habits.length;

  // Recent tasks
  const rt = document.getElementById('recent-tasks');
  const recent = [...state.tasks].reverse().slice(0, 5);
  if (recent.length === 0) { rt.innerHTML = '<p class="empty-msg">Henüz görev yok.</p>'; }
  else {
    rt.innerHTML = recent.map(t => {
      const col = t.priority === 'Yüksek' ? 'var(--red)' : t.priority === 'Düşük' ? 'var(--green)' : 'var(--blue)';
      return `<div class="recent-item">
        <span class="recent-dot" style="background:${col}"></span>
        <span class="recent-text">${t.title}</span>
        <span class="recent-meta">${statusEmoji(t.status)}</span>
      </div>`;
    }).join('');
  }

  // Recent habits
  const rh = document.getElementById('recent-habits');
  if (state.habits.length === 0) { rh.innerHTML = '<p class="empty-msg">Henüz alışkanlık yok.</p>'; }
  else {
    rh.innerHTML = [...state.habits].sort((a,b) => b.streak-a.streak).slice(0,5).map(h =>
      `<div class="recent-item">
        <span class="recent-dot" style="background:var(--orange)"></span>
        <span class="recent-text">${h.name}</span>
        <span class="recent-meta">🔥 ${h.streak} gün</span>
      </div>`
    ).join('');
  }
}

function statusEmoji(s) {
  if (s === 'Tamamlandı') return '✅';
  if (s === 'Devam Ediyor') return '🔄';
  return '⏳';
}

// ══════════════════════════════════════════
// TASKS
// ══════════════════════════════════════════
document.getElementById('task-due').value = today();

document.getElementById('btn-add-task').addEventListener('click', () => {
  const title = document.getElementById('task-title').value.trim();
  if (!title) { showToast('⚠️ Görev başlığı boş bırakılamaz!', 'var(--orange)'); return; }
  const task = {
    id: state.nextTaskId++,
    title,
    priority: document.getElementById('task-priority').value,
    status:   document.getElementById('task-status').value,
    due:      document.getElementById('task-due').value || today(),
    createdAt: now(),
  };
  state.tasks.push(task);
  save();
  document.getElementById('task-title').value = '';
  renderTasks();
  renderDashboard();
  showToast('✅ Görev eklendi!');
});

// Enter ile ekle
document.getElementById('task-title').addEventListener('keydown', e => {
  if (e.key === 'Enter') document.getElementById('btn-add-task').click();
});

// Durum filtresi
document.querySelectorAll('.filter-btn').forEach(btn => {
  btn.addEventListener('click', () => {
    document.querySelectorAll('.filter-btn').forEach(b => b.classList.remove('active'));
    btn.classList.add('active');
    state.taskFilter = btn.dataset.filter;
    renderTasks();
  });
});

// Canlı arama
document.getElementById('task-search').addEventListener('input', e => {
  state.taskSearchQuery = e.target.value.trim().toLowerCase();
  renderTasks();
});

// Öncelik filtresi
document.getElementById('task-priority-filter').addEventListener('change', e => {
  state.taskPriorityFilter = e.target.value;
  renderTasks();
});

// Geri al butonu (Stack pop)
document.getElementById('btn-undo-delete').addEventListener('click', () => {
  if (deletedTasksStack.length === 0) {
    showToast('⚠️ Geri alınacak silinmiş görev yok.', 'var(--orange)');
    return;
  }
  const restored = deletedTasksStack.pop();
  state.tasks.push(restored);
  save();
  renderTasks();
  renderDashboard();
  showToast(`↩ "${restored.title}" geri getirildi.`, 'var(--blue)');
});

// Güncelle butonu
document.getElementById('btn-update-task').addEventListener('click', () => {
  const title = document.getElementById('task-title').value.trim();
  if (!title) { showToast('⚠️ Görev başlığı boş bırakılamaz!', 'var(--orange)'); return; }
  const task = state.tasks.find(t => t.id === state.editingTaskId);
  if (!task) return;
  task.title    = title;
  task.priority = document.getElementById('task-priority').value;
  task.status   = document.getElementById('task-status').value;
  task.due      = document.getElementById('task-due').value || today();
  save();
  cancelEditMode();
  renderTasks();
  renderDashboard();
  showToast('💾 Görev güncellendi!');
});

// İptal butonu
document.getElementById('btn-cancel-edit').addEventListener('click', cancelEditMode);

function renderTasks() {
  const tbody = document.getElementById('task-tbody');

  // Durum, arama ve öncelik filtrelerini birlikte uygula
  let tasks = state.tasks
    .filter(t => state.taskFilter === 'all' || t.status === state.taskFilter)
    .filter(t => state.taskPriorityFilter === 'all' || t.priority === state.taskPriorityFilter)
    .filter(t => !state.taskSearchQuery || t.title.toLowerCase().includes(state.taskSearchQuery));

  if (tasks.length === 0) {
    tbody.innerHTML = '<tr class="empty-row"><td colspan="6">Görev bulunamadı.</td></tr>';
    return;
  }

  tbody.innerHTML = tasks.map(t => `
    <tr id="task-row-${t.id}">
      <td><span style="color:var(--text-muted);font-size:.8rem">${t.id}</span></td>
      <td><strong>${escHtml(t.title)}</strong></td>
      <td>${priorityBadge(t.priority)}</td>
      <td>${statusBadge(t.status)}</td>
      <td style="color:var(--text-secondary)">${fmtDate(t.due)}</td>
      <td>
        <div style="display:flex;gap:.4rem">
          <button class="btn-icon btn-icon-edit" onclick="startEditTask(${t.id})" title="Düzenle">✏️</button>
          <button class="btn-icon" onclick="cycleTaskStatus(${t.id})" title="Durumu değiştir">🔄</button>
          <button class="btn-icon" onclick="deleteTask(${t.id})" title="Sil">🗑️</button>
        </div>
      </td>
    </tr>
  `).join('');

  // mouseenter / mouseleave ile satır vurgulama (JS ile)
  tbody.querySelectorAll('tr[id^="task-row-"]').forEach(row => {
    row.addEventListener('mouseenter', () => {
      row.style.outline = '1px solid rgba(137,180,250,.35)';
    });
    row.addEventListener('mouseleave', () => {
      row.style.outline = '';
    });
  });
}

function priorityBadge(p) {
  const m = { 'Yüksek': 'badge-high', 'Normal': 'badge-normal', 'Düşük': 'badge-low' };
  const e = { 'Yüksek': '🔴', 'Normal': '🔵', 'Düşük': '🟢' };
  return `<span class="badge ${m[p]||'badge-normal'}">${e[p]||''} ${p}</span>`;
}
function statusBadge(s) {
  const m = { 'Bekliyor': 'badge-waiting', 'Devam Ediyor': 'badge-progress', 'Tamamlandı': 'badge-done' };
  return `<span class="badge ${m[s]||'badge-waiting'}">${statusEmoji(s)} ${s}</span>`;
}

function cycleTaskStatus(id) {
  const t = state.tasks.find(x => x.id === id);
  if (!t) return;
  const cycle = ['Bekliyor', 'Devam Ediyor', 'Tamamlandı'];
  t.status = cycle[(cycle.indexOf(t.status) + 1) % cycle.length];
  save(); renderTasks(); renderDashboard();
  showToast(`🔄 Durum: ${t.status}`);
}

function deleteTask(id) {
  const t = state.tasks.find(x => x.id === id);
  showConfirm('Görevi Sil', `"${t?.title}" silinecek. Emin misin?`, () => {
    // Silmeden önce Stack'e push et
    if (t) deletedTasksStack.push({ ...t });
    state.tasks = state.tasks.filter(x => x.id !== id);
    save(); renderTasks(); renderDashboard();
    showToast('🗑️ Görev silindi.', 'var(--red)');
  });
}

// Düzenleme modunu aç: formu seçilen görevin verileriyle doldurur
function startEditTask(id) {
  const task = state.tasks.find(t => t.id === id);
  if (!task) return;
  state.editingTaskId = id;
  document.getElementById('task-title').value    = task.title;
  document.getElementById('task-priority').value = task.priority;
  document.getElementById('task-status').value   = task.status;
  document.getElementById('task-due').value      = task.due;
  document.querySelector('#task-form-card .form-title').textContent = '✏️ Görevi Düzenle';
  document.getElementById('btn-add-task').classList.add('hidden');
  document.getElementById('btn-update-task').classList.remove('hidden');
  document.getElementById('btn-cancel-edit').classList.remove('hidden');
  document.getElementById('task-title').focus();
  document.getElementById('task-form-card').scrollIntoView({ behavior: 'smooth', block: 'nearest' });
}

// Düzenleme modundan çıkış: formu sıfırlar
function cancelEditMode() {
  state.editingTaskId = null;
  document.getElementById('task-title').value    = '';
  document.getElementById('task-priority').value = 'Normal';
  document.getElementById('task-status').value   = 'Bekliyor';
  document.getElementById('task-due').value      = today();
  document.querySelector('#task-form-card .form-title').textContent = 'Yeni Görev';
  document.getElementById('btn-add-task').classList.remove('hidden');
  document.getElementById('btn-update-task').classList.add('hidden');
  document.getElementById('btn-cancel-edit').classList.add('hidden');
}

// ══════════════════════════════════════════
// HABITS
// ══════════════════════════════════════════
document.getElementById('btn-add-habit').addEventListener('click', () => {
  const name = document.getElementById('habit-name').value.trim();
  if (!name) { showToast('⚠️ Alışkanlık adı boş bırakılamaz!', 'var(--orange)'); return; }
  state.habits.push({ id: state.nextHabitId++, name, streak: 0, lastChecked: '', createdAt: now() });
  save();
  document.getElementById('habit-name').value = '';
  renderHabits(); renderDashboard();
  showToast('🔥 Alışkanlık eklendi!');
});
document.getElementById('habit-name').addEventListener('keydown', e => {
  if (e.key === 'Enter') document.getElementById('btn-add-habit').click();
});

function renderHabits() {
  const container = document.getElementById('habit-cards');
  if (state.habits.length === 0) {
    container.innerHTML = '<div class="empty-card">Henüz alışkanlık eklenmedi. Yukarıdan başla! 🚀</div>';
    return;
  }
  container.innerHTML = state.habits.map(h => {
    const checkedToday = h.lastChecked === today();
    return `<div class="habit-card">
      <div class="habit-card-top">
        <div>
          <div class="habit-name">${escHtml(h.name)}</div>
          <div class="habit-streak">
            <span class="streak-num">${h.streak}</span>
            <span class="streak-label">gün seri</span>
            <span class="streak-fire">${h.streak >= 7 ? '🔥' : h.streak >= 3 ? '✨' : '⭐'}</span>
          </div>
        </div>
        <button class="btn-icon" onclick="deleteHabit(${h.id})" title="Sil">🗑️</button>
      </div>
      <div class="habit-card-bottom">
        <span class="habit-last">Son: ${h.lastChecked ? fmtDate(h.lastChecked) : 'Hiç yapılmadı'}</span>
        <button class="btn-check" onclick="checkHabit(${h.id})" ${checkedToday ? 'disabled style="opacity:.4;cursor:not-allowed"' : ''}>
          ${checkedToday ? '✅ Yapıldı' : '✔ Bugün Yap'}
        </button>
      </div>
    </div>`;
  }).join('');
}

function checkHabit(id) {
  const h = state.habits.find(x => x.id === id);
  if (!h || h.lastChecked === today()) return;
  // Arka arkaya mı?
  const yesterday = new Date(); yesterday.setDate(yesterday.getDate() - 1);
  const yStr = yesterday.toISOString().split('T')[0];
  h.streak = (h.lastChecked === yStr) ? h.streak + 1 : 1;
  h.lastChecked = today();
  save(); renderHabits(); renderDashboard();
  showToast(`🔥 ${h.name} — ${h.streak} gün seri!`);
}

function deleteHabit(id) {
  const h = state.habits.find(x => x.id === id);
  showConfirm('Alışkanlığı Sil', `"${h?.name}" silinecek. Emin misin?`, () => {
    state.habits = state.habits.filter(x => x.id !== id);
    save(); renderHabits(); renderDashboard();
    showToast('🗑️ Alışkanlık silindi.', 'var(--red)');
  });
}

// ══════════════════════════════════════════
// FOCUS / POMODORO
// ══════════════════════════════════════════
const RING_CIRCUMFERENCE = 2 * Math.PI * 88; // r=88
let focusTotalSeconds = 25 * 60;
let focusRemaining    = focusTotalSeconds;
let focusRunning      = false;
let focusInterval     = null;
let focusStart        = null;

const ringEl    = document.getElementById('ring-progress');
const timerEl   = document.getElementById('focus-timer');
const statusEl  = document.getElementById('focus-status');
const startBtn  = document.getElementById('btn-focus-start');
const resetBtn  = document.getElementById('btn-focus-reset');

// SVG gradient tanımı
const svgNs = 'http://www.w3.org/2000/svg';
const defs  = document.createElementNS(svgNs, 'defs');
const grad  = document.createElementNS(svgNs, 'linearGradient');
grad.setAttribute('id', 'grad1');
grad.setAttribute('x1','0%'); grad.setAttribute('y1','0%');
grad.setAttribute('x2','100%'); grad.setAttribute('y2','100%');
const s1 = document.createElementNS(svgNs, 'stop');
s1.setAttribute('offset','0%'); s1.setAttribute('stop-color','#fab387');
const s2 = document.createElementNS(svgNs, 'stop');
s2.setAttribute('offset','100%'); s2.setAttribute('stop-color','#f38ba8');
grad.appendChild(s1); grad.appendChild(s2); defs.appendChild(grad);
document.querySelector('.focus-ring').prepend(defs);
ringEl.style.strokeDasharray  = RING_CIRCUMFERENCE;
ringEl.style.strokeDashoffset = RING_CIRCUMFERENCE;

function updateTimer() {
  const m = Math.floor(focusRemaining / 60).toString().padStart(2,'0');
  const s = (focusRemaining % 60).toString().padStart(2,'0');
  timerEl.textContent = `${m}:${s}`;
  const pct = (focusTotalSeconds - focusRemaining) / focusTotalSeconds;
  ringEl.style.strokeDashoffset = RING_CIRCUMFERENCE * (1 - pct);
}

startBtn.addEventListener('click', () => {
  if (!focusRunning) {
    if (focusRemaining === focusTotalSeconds) focusStart = new Date();
    focusRunning = true;
    startBtn.textContent = '⏸ Duraklat';
    statusEl.textContent = '🎯 Odaklanıyorsun!';
    focusInterval = setInterval(() => {
      if (focusRemaining > 0) {
        focusRemaining--;
        updateTimer();
      } else {
        // Tamamlandı
        clearInterval(focusInterval); focusInterval = null;
        focusRunning = false;
        const session = {
          id: Date.now(),
          duration: Math.round(focusTotalSeconds / 60),
          startTime: focusStart?.toISOString() || now(),
          endTime: now(),
        };
        state.sessions.push(session);
        save();
        renderSessions(); renderDashboard();
        statusEl.textContent = '✅ Tamamlandı!';
        startBtn.textContent = '▶ Başlat';
        focusRemaining = focusTotalSeconds;
        updateTimer();
        showToast(`🎉 ${session.duration} dakikalık odak seansı tamamlandı!`, 'var(--orange)');
      }
    }, 1000);
  } else {
    clearInterval(focusInterval); focusInterval = null;
    focusRunning = false;
    startBtn.textContent = '▶ Devam Et';
    statusEl.textContent = '⏸ Duraklatıldı';
  }
});

resetBtn.addEventListener('click', () => {
  clearInterval(focusInterval); focusInterval = null;
  focusRunning = false;
  focusRemaining = focusTotalSeconds;
  updateTimer();
  startBtn.textContent = '▶ Başlat';
  statusEl.textContent = 'Hazır';
});

document.querySelectorAll('.dur-btn').forEach(btn => {
  btn.addEventListener('click', () => {
    if (focusRunning) return;
    document.querySelectorAll('.dur-btn').forEach(b => b.classList.remove('active'));
    btn.classList.add('active');
    focusTotalSeconds = parseInt(btn.dataset.min) * 60;
    focusRemaining    = focusTotalSeconds;
    updateTimer();
    statusEl.textContent = 'Hazır';
  });
});

function renderSessions() {
  const list = document.getElementById('session-list');
  document.getElementById('session-count').textContent = `${state.sessions.length} seans`;
  if (state.sessions.length === 0) {
    list.innerHTML = '<p class="empty-msg">Henüz tamamlanan seans yok.</p>';
    return;
  }
  list.innerHTML = [...state.sessions].reverse().map(s =>
    `<div class="session-item">
      <span class="session-icon">🍅</span>
      <div class="session-info">
        <div class="session-dur">${s.duration} dakika odak</div>
        <div class="session-time">${new Date(s.endTime).toLocaleString('tr-TR')}</div>
      </div>
    </div>`
  ).join('');
}

// ── UTIL ──
function escHtml(str) {
  return str.replace(/&/g,'&amp;').replace(/</g,'&lt;').replace(/>/g,'&gt;').replace(/"/g,'&quot;');
}

// ── INIT ──
updateTimer();
